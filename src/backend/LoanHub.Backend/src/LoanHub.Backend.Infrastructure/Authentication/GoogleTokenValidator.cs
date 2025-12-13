
using LoanHub.Backend.Core.Interfaces;

namespace LoanHub.Backend.Infrastructure.Authentication;

public sealed class GoogleTokenValidator(
	IConfiguration configuration,
	ILogger<GoogleTokenValidator> logger) : IGoogleTokenValidator
{
	private readonly string _clientId = configuration["Authentication:Google:ClientId"]
		?? throw new InvalidOperationException("Google ClientId not configured");

	public async Task<GoogleTokenValidationResult> ValidateTokenAsync(string token)
	{
		try
		{
			var payload = await GoogleJsonWebSignature.ValidateAsync(token, new GoogleJsonWebSignature.ValidationSettings
			{
				Audience = [_clientId]
			});

			logger.LogInformation("Successfully validated Google token for user: {Email}", payload.Email);

			return GoogleTokenValidationResult.Success(
				payload.Email,
				payload.GivenName ?? string.Empty,
				payload.FamilyName ?? string.Empty);
		}
		catch (InvalidJwtException ex)
		{
			logger.LogWarning("Invalid Google JWT token: {Error}", ex.Message);
			return GoogleTokenValidationResult.Failure("Invalid Google token");
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "Error validating Google token");
			return GoogleTokenValidationResult.Failure("Token validation failed");
		}
	}
}