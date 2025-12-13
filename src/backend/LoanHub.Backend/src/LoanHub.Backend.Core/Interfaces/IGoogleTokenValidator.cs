namespace LoanHub.Backend.Core.Interfaces;

public interface IGoogleTokenValidator
{
	public Task<GoogleTokenValidationResult> ValidateTokenAsync(string token);
}