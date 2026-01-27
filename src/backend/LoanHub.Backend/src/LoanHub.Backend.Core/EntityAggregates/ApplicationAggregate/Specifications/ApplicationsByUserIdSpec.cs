namespace LoanHub.Backend.Core.EntityAggregates.ApplicationAggregate.Specifications;

public sealed class ApplicationsByUserIdSpec : Specification<Application>
{
	public ApplicationsByUserIdSpec(int userId)
	{
		Query.Where(a => a.UserId == userId);
	}
}