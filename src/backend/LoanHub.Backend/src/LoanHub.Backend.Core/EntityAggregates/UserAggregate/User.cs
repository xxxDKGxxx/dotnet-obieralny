namespace LoanHub.Backend.Core.EntityAggregates.UserAggregate;

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
	public string? Address { get; private set; }
	public string? Phone { get; private set; }
	public string? Job { get; private set; }
	public decimal? Income { get; private set; }
	public decimal? Costs { get; private set; }
	public int? Age { get; private set; }
	public int? Dependents { get; private set; }

	public void UpdateFirstName(string? firstName)
	{
		if (!string.IsNullOrWhiteSpace(firstName))
		{
			FirstName = firstName;
		}
	}

	public void UpdateLastName(string? lastName)
	{
		if (!string.IsNullOrWhiteSpace(lastName))
		{
			LastName = lastName;
		}
	}

	public void UpdateAddress(string? address)
	{
		Address = address;
	}

	public void UpdatePhone(string? phone)
	{
		Phone = phone;
	}

	public void UpdateJob(string? job)
	{
		Job = job;
	}

	public void UpdateIncome(decimal? income)
	{
		if (income is not null and < 0)
		{
			throw new ArgumentOutOfRangeException(nameof(income), $"Income cannot be negative. Value: {income}");
		}

		Income = income;
	}

	public void UpdateCosts(decimal? costs)
	{
		if (costs is not null and < 0)
		{
			throw new ArgumentOutOfRangeException(nameof(costs), $"Costs cannot be negative. Value: {costs}");
		}

		Costs = costs;
	}

	public void UpdateAge(int? age)
	{
		if (age is not null and (< UserConstants.MinAge or > UserConstants.MaxAge))
		{
			throw new ArgumentOutOfRangeException(nameof(age), $"Age must be between {UserConstants.MinAge} and {UserConstants.MaxAge}. Value: {age}");
		}

		Age = age;
	}

	public void UpdateDependents(int? dependents)
	{
		if (dependents is not null)
		{
			if (dependents < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(dependents), $"Dependents cannot be negative. Value: {dependents}");
			}

			if (dependents > UserConstants.MaxDependents)
			{
				throw new ArgumentOutOfRangeException(nameof(dependents), $"Dependents cannot exceed {UserConstants.MaxDependents}. Value: {dependents}");
			}
		}
		Dependents = dependents;
	}
}