using LoanHub.Backend.UseCases.Features.Authentication.Login;

namespace LoanHub.Backend.UseCases.Features.Authentication.Test;

public sealed class TestLoginHandler(
	IRepository<UserEntity> userRepository,
	ITokenProvider tokenProvider,
	ILogger<LoginHandler> logger) : ICommandHandler<TestLoginCommand, Result<TokenDto>>
{
	public async Task<Result<TokenDto>> Handle(
		TestLoginCommand request,
		CancellationToken cancellationToken)
	{
		var userByEmailSpec = new UserByEmailSpec(request.Email);
		var existingUser = await userRepository.FirstOrDefaultAsync(userByEmailSpec, cancellationToken);

		if (existingUser is null)
		{
			return Result.NotFound();
		}
		else
		{
			logger.LogInformation("Test user logged in: {Email}", existingUser.Email);

			var accessToken = tokenProvider.GenerateToken(existingUser);
			return Result.Success(new TokenDto(accessToken));
		}
	}
}