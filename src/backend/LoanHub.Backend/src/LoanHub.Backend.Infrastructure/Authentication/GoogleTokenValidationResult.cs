namespace LoanHub.Backend.Core.Interfaces;

public sealed class GoogleTokenValidationResult
{
	public bool IsValid { get; init; }
	public string? Email { get; init; }
	public string? FirstName { get; init; }
	public string? LastName { get; init; }
	public string? ErrorMessage { get; init; }

	public static GoogleTokenValidationResult Success(string email, string firstName, string lastName)
	{
		return new() { IsValid = true, Email = email, FirstName = firstName, LastName = lastName };
	}

	public static GoogleTokenValidationResult Failure(string errorMessage)
	{
		return new() { IsValid = false, ErrorMessage = errorMessage };
	}
}