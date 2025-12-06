namespace LoanHub.Backend.Core.UserAggregate;

public sealed class User(
	string email,
	string firstName,
	string lastName,
	UserRole role) :
	LoanHubEntityBase,
	IAggregateRoot
{
	public string Email { get; private set; } = email;
	public UserRole Role { get; private set; } = role;
	public string FirstName { get; private set; } = firstName;
	public string LastName { get; private set; } = lastName;
	public string? Address
	{
		get; private set;
	}
	public string? Phone
	{
		get; private set;
	}
	public string? Job
	{
		get; private set;
	}
	public decimal? Income
	{
		get; private set;
	}
	public decimal? Costs
	{
		get; private set;
	}
	public int? Age
	{
		get; private set;
	}
	public int? Dependents
	{
		get; private set;
	}

	private void SetIncome(decimal? income)
	{
		if (income is not null)
		{
			if (income < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(income),
					$"Income cannot be negative. Value: {income}");
		}
		}
		Income = income;
		
	}

	private void SetCosts(decimal? costs)
	{
		if (costs is not null)
		{
			if (costs < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(costs),
					$"Costs cannot be negative. Value: {costs}");
			}
		}

		Costs = costs;
	}

	private void SetAge(int? age)
	{
		if (age is not null && age is < UserConstants.MinAge or > UserConstants.MaxAge)
		{
			throw new ArgumentOutOfRangeException(nameof(age),
				$"Age must be between {UserConstants.MinAge} and {UserConstants.MaxAge}. Value: {age}");
		}

		Age = age;
	}

	private void SetDependents(int? dependents)
	{
		if (dependents is not null)
		{
			if (dependents < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(dependents),
					$"Dependents cannot be negative. Value: {dependents}");
			}

			if (dependents > UserConstants.MaxDependents)
			{
				throw new ArgumentOutOfRangeException(nameof(dependents),
					$"Dependents cannot exceed {UserConstants.MaxDependents}. Value: {dependents}");
			}
		}

		Dependents = dependents;
	}
}	