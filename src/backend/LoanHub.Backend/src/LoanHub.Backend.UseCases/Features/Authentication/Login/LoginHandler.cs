using LoanHub.Backend.UseCases.Interfaces;

namespace LoanHub.Backend.UseCases.Features.Authentication.Login;

public sealed class LoginHandler(
	IEnumerable<ILoginProvider> loginProviders,
	ITokenProvider tokenProvider,
	IRepository<User> userRepository,
	ILogger<LoginHandler> logger) : IRequestHandler<LoginCommand, Result<TokenDto>>
{
	public async Task<Result<TokenDto>> Handle(
		LoginCommand request,
		CancellationToken cancellationToken)
	{
		var selectedLoginProvider = loginProviders.SingleOrDefault(p =>
		{
			return p.Type == request.Type;
		});

		if (selectedLoginProvider == null)
		{
			logger.LogWarning("Login provider not found for type: {LoginType}", request.Type);

			return Result.NotFound($"Login provider for {request.Type} not found");
		}

		try
		{
			var externalUserDto = await selectedLoginProvider.AuthenticateAsync(request.Token, cancellationToken);

			var userByEmailSpec = new UserByEmailSpec(externalUserDto.Email);
			var existingUser = await userRepository.FirstOrDefaultAsync(userByEmailSpec, cancellationToken);

			if (existingUser is null)
			{
				var newUser = new User(
					externalUserDto.Email,
					externalUserDto.FirstName,
					externalUserDto.LastName,
						UserRole.User);

				await userRepository.AddAsync(newUser, cancellationToken);
				await userRepository.SaveChangesAsync(cancellationToken);

				logger.LogInformation("Created new user: {Email} via {LoginType}", newUser.Email, request.Type);

				var accessToken = tokenProvider.GenerateToken(newUser);
				return Result.Success(new TokenDto(accessToken));
			}
			else
			{
				logger.LogInformation("User logged in: {Email} via {LoginType}", existingUser.Email, request.Type);

				var accessToken = tokenProvider.GenerateToken(existingUser);
				return Result.Success(new TokenDto(accessToken));
			}
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "Login failed for type {LoginType}", request.Type);
			return Result.Unauthorized();
		}
	}
}