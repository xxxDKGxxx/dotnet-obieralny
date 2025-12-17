namespace LoanHub.Backend.Infrastructure.Authentication;

public sealed class GoogleLoginProvider(
	IConfiguration configuration,
	ILogger<GoogleLoginProvider> logger) : ILoginProvider
{
	private readonly string _clientId = configuration["Authentication:Google:ClientId"]
		?? throw new InvalidOperationException("Google ClientId not configured");

	public LoginType Type => LoginType.Google;

	public async Task<User> AuthenticateAsync(string token, CancellationToken cancellationToken = default)
	{
		try
		{
			var payload = await GoogleJsonWebSignature.ValidateAsync(token, new GoogleJsonWebSignature.ValidationSettings
			{
				Audience = [_clientId]
			});

			logger.LogInformation("Successfully validated Google token for user: {Email}", payload.Email);

			return new User(
				payload.Email,
				payload.GivenName ?? string.Empty,
				payload.FamilyName ?? string.Empty,
				UserRole.User);
		}
		catch (InvalidJwtException ex)
		{
			logger.LogWarning("Invalid Google JWT token: {Error}", ex.Message);
			throw new UnauthorizedAccessException("Invalid Google token", ex);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "Error validating Google token");
			throw new InvalidOperationException("Token validation failed", ex);
		}
	}
}