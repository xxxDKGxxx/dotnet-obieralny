namespace LoanHub.Backend.UseCases.Features.Authentication.Login;

public sealed class LoginHandler(
	IEnumerable<ILoginProvider> loginProviders,
	ITokenProvider tokenProvider,
	IRepository<User> userRepository,
	ILogger<LoginHandler> logger) : IRequestHandler<LoginCommand, Result<LoginResult>>
{
	public async Task<Result<LoginResult>> Handle(
		LoginCommand request,
		CancellationToken cancellationToken)
	{
		var provider = loginProviders.SingleOrDefault(p =>
		{
			return p.Type == request.Type;
		});
		if (provider == null)
		{
			logger.LogWarning("Login provider not found for type: {LoginType}", request.Type);
			return Result.NotFound($"Login provider for {request.Type} not found");
		}

		try
		{
			var authenticatedUser = await provider.AuthenticateAsync(request.Token, cancellationToken);

			var existingUser = await userRepository.FirstOrDefaultAsync(
				new UserByEmailSpec(authenticatedUser.Email), cancellationToken);

			User user;
			if (existingUser is null)
			{
				user = authenticatedUser;
				await userRepository.AddAsync(user, cancellationToken);
				await userRepository.SaveChangesAsync(cancellationToken);

				logger.LogInformation("Created new user: {Email} via {LoginType}", user.Email, request.Type);
			}
			else
			{
				user = existingUser;
				logger.LogInformation("User logged in: {Email} via {LoginType}", user.Email, request.Type);
			}

			var accessToken = tokenProvider.GenerateToken(user);

			return Result.Success(new LoginResult(accessToken));
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "Login failed for type {LoginType}", request.Type);
			return Result.Unauthorized();
		}
	}
}