namespace LoanHub.Backend.Web.Endpoints.Shared;

public sealed class UserDto
{
	public required int Id { get; init; }
	public required string Email { get; init; }
	public required string FirstName { get; init; }
	public string? LastName { get; init; }
	public required string Role { get; init; }
}