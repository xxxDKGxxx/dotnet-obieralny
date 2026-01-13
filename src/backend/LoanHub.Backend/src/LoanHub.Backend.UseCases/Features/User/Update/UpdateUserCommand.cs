namespace LoanHub.Backend.UseCases.Features.CurrentUser.UpdateUser;

public sealed record UpdateUserCommand(
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
	) : ICommand<Result<UserProfileDto>>;