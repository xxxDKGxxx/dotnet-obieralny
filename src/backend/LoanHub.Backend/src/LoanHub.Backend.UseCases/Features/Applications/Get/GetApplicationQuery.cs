namespace LoanHub.Backend.UseCases.Features.Applications.Get;

public record GetApplicationQuery(int ApplicationId) : IQuery<Result<ApplicationDTO>>;
