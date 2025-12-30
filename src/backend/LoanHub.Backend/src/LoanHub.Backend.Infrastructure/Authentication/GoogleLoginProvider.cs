namespace LoanHub.Backend.Infrastructure.Authentication;

public sealed class GoogleLoginProvider(
	string clientId,
	ILogger<GoogleLoginProvider> logger) :
	ILoginProvider
{
	private readonly string _clientId = clientId;

	public LoginType Type
	{
		get { return LoginType.Google; }
	}

	public async Task<ExternalUserDto> AuthenticateAsync(string token, CancellationToken cancellationToken = default)
	{
		var validationSettings = new GoogleJsonWebSignature.ValidationSettings
		{
			Audience = [_clientId]
		};

		var payload = await GoogleJsonWebSignature.ValidateAsync(token, validationSettings);

		if (payload?.Email == null)
		{
			logger.LogWarning("Google token validation returned null or missing email");
			throw new UnauthorizedAccessException("Invalid Google token - missing email");
		}

		logger.LogInformation("Successfully validated Google token for user: {Email}", payload.Email);

		return new ExternalUserDto(
			payload.Email,
			payload.GivenName ?? string.Empty,
			payload.FamilyName ?? string.Empty);
	}
}