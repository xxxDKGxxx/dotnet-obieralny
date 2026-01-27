namespace LoanHub.Backend.UseCases.Features.Audit.List;

public sealed record ListAuditsFromSpecificDayQuery(DateTime Day) : IQuery<Result<IEnumerable<AuditDto>>>;