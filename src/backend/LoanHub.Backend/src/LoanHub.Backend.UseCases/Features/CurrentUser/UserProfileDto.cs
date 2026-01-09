namespace LoanHub.Backend.UseCases.Features.CurrentUser.GetCurrentUser;

public sealed class UserProfileDto
{
	public required int Id { get; init; }
	public required string Email { get; init; }
	public required string FirstName { get; init; }
	public string? LastName { get; init; }
	public required string Role { get; init; }
	public string? Address { get; init; }
	public string? Phone { get; init; }
	public string? Job { get; init; }
	public decimal? Income { get; init; }
	public decimal? Costs { get; init; }
	public int? Age { get; init; }
	public int? Dependents { get; init; }
}