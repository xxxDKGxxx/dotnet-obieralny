namespace LoanHub.Backend.UseCases.Features.Authentication.Login;

public sealed record ExternalUserDto(
	string Email,
	string FirstName,
	string LastName);