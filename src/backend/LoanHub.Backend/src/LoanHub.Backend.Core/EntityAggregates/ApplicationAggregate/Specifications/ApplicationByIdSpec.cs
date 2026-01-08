namespace LoanHub.Backend.Core.EntityAggregates.ApplicationAggregate.Specifications;
public class ApplicationByIdSpec : Specification<Application>
{
	public ApplicationByIdSpec(int applicationId) =>
	  Query
		  .Where(application => application.Id == applicationId);
}