namespace LoanHub.Backend.Core.EntityAggregates.ApplicationAggregate.Specifications;
<<<<<<< HEAD

public sealed class ApplicationByIdSpec : SingleResultSpecification<Application>
{
	public ApplicationByIdSpec(int applicationId)
	{
		Query.Where(a => a.Id == applicationId);
	}
=======
public class ApplicationByIdSpec : Specification<Application>
{
	public ApplicationByIdSpec(int applicationId) =>
	  Query
		  .Where(application => application.Id == applicationId);
>>>>>>> 7ad7476eeea0e2f4c266a6307f360e22ed70bb07
}