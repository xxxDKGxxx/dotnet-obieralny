namespace LoanHub.Backend.Web.Endpoints.User;

public sealed record UpdateUserRequest(
	int UserId,
	string? FirstName,
	string? LastName,
	string? Address,
	string? Phone,
	string? Job,
	decimal? Income,
	decimal? Costs,
	int? Age,
	int? Dependents
);