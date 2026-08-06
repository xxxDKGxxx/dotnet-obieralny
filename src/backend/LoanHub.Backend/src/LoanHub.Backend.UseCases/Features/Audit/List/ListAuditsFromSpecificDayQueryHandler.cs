namespace LoanHub.Backend.UseCases.Features.Audit.List;

public class ListAuditsFromSpecificDayQueryHandler(
	IReadRepository<AuditEntity> auditRepository,
	IMapper mapper) :
	IQueryHandler<ListAuditsFromSpecificDayQuery,
	Result<IEnumerable<AuditDto>>>
{
	public async Task<Result<IEnumerable<AuditDto>>> Handle(
		ListAuditsFromSpecificDayQuery request,
		CancellationToken cancellationToken)
	{
		var spec = new AuditsByDaySpec(request.Day);
		var audits = await auditRepository.ListAsync(spec, cancellationToken);
		var auditDtos = audits.Select(mapper.Map<AuditDto>);

		return Result.Success(auditDtos);
	}
}