using LoanHub.Backend.UseCases.Features.Authentication.Login;

namespace LoanHub.Backend.UseCases.Features.Authentication.Test;

public sealed class TestLoginHandler(
	IRepository<UserEntity> userRepository,
	ITokenProvider tokenProvider,
	ILogger<LoginHandler> logger) : IRequestHandler<TestLoginCommand, Result<TokenDto>>
{
	public async Task<Result<TokenDto>> Handle(
	TestLoginCommand request,
	CancellationToken cancellationToken)
	{
		try
		{
			var userByEmailSpec = new UserByEmailSpec(request.Email);
			var existingUser = await userRepository.FirstOrDefaultAsync(userByEmailSpec, cancellationToken);

			if (existingUser is null)
			{
				throw new Exception("No requested test user in database");
			}
			else
			{
				logger.LogInformation("Test user logged in: {Email}", existingUser.Email);

				var accessToken = tokenProvider.GenerateToken(existingUser);
				return Result.Success(new TokenDto(accessToken));
			}
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "Test user login failed");
			return Result.Unauthorized();
		}
	}
}