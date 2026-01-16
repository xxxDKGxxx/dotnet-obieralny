namespace LoanHub.Backend.UseCases.Features.Application.Get;

public sealed record GetApplicationByIdQuery(int ApplicationIId) : IQuery<Result<ApplicationDto>>;