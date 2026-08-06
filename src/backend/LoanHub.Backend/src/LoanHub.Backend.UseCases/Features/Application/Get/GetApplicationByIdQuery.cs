namespace LoanHub.Backend.UseCases.Features.Application.Get;

public sealed record GetApplicationByIdQuery(int ApplicationId, int RequestingUserId) : IQuery<Result<ApplicationDto>>;