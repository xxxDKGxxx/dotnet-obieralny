namespace LoanHub.Backend.Core.EntityAggregates.ApplicationAggregate.Specifications;

public sealed class ApplicationByIdSpec : SingleResultSpecification<Application>
{
	public ApplicationByIdSpec(int applicationId)
	{
		Query.Where(a => a.Id == applicationId);
	}
}