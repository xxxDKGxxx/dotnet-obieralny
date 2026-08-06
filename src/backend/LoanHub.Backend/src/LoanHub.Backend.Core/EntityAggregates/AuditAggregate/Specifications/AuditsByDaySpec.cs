namespace LoanHub.Backend.Core.EntityAggregates.AuditAggregate.Specifications;

public class AuditsByDaySpec : Specification<Audit>
{
	public AuditsByDaySpec(DateTime day)
	{
		Query.Where(a => a.CreatedAt >= day.Date && a.CreatedAt <= day.Date.AddDays(1));
	}
}