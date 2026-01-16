namespace LoanHub.Backend.UseCases.Features.Application.Get;

public sealed record GetApplicationByIdQuery(int ApplicationId) : IQuery<Result<ApplicationDto>>;