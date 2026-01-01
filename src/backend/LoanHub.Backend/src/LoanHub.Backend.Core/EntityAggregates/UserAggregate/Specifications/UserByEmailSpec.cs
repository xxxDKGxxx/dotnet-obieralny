namespace LoanHub.Backend.Core.UserAggregate.Specifications;

public sealed class UserByEmailSpec : Specification<User>
{
	public UserByEmailSpec(string email)
	{
		Query.Where(u => u.Email == email);
	}
}