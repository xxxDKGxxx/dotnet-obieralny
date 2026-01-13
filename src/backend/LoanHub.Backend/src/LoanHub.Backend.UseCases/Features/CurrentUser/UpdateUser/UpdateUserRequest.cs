namespace LoanHub.Backend.UseCases.Features.CurrentUser.UpdateUser;

public sealed class UpdateUserRequest
{
	public string? FirstName { get; set; }
	public string? LastName { get; set; }
	public string? Address { get; set; }
	public string? Phone { get; set; }
	public string? Job { get; set; }
	public decimal? Income { get; set; }
	public decimal? Costs { get; set; }
	public int? Age { get; set; }
	public int? Dependents { get; set; }
}