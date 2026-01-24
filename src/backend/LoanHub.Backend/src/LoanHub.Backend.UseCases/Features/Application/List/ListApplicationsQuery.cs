namespace LoanHub.Backend.UseCases.Features.Application.List;

public sealed record ListApplicationsQuery(
	int? UserId,
	int RequestingUserId) :
	IQuery<Result<IEnumerable<ApplicationDto>>>;