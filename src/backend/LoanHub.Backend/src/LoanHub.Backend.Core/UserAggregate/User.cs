namespace LoanHub.Backend.Core.UserAggregate;

public sealed class User(
	string email,
	string firstName,
	string lastName,
	UserRole role) :
	LoanHubEntityBase,
	IAggregateRoot
{

	private decimal? _income;
	private decimal? _costs;
	private int? _age;
	private int? _dependents;

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
		get
		{
			return _income;
		}

		private set
		{
			_income = ValidateFinancialAmount(value, nameof(Income));
		}
	}
	public decimal? Costs
	{
		get
		{
			return _costs;
		}

		private set
		{
			_costs = ValidateFinancialAmount(value, nameof(Costs));
		}
	}
	public int? Age
	{
		get
		{
			return _age;
		}

		private set
		{
			_age = ValidateAge(value);
		}
	}

	public int? Dependents
	{
		get
		{
			return _dependents;
		}

		private set
		{
			_dependents = ValidateDependents(value);
		}
	}

	public void SetIncome(decimal? income)
	{
		Income = income;
	}

	public void SetCosts(decimal? costs)
	{
		Costs = costs;
	}

	public void SetAge(int? age)
	{
		Age = age;
	}

	public void SetDependents(int? dependents)
	{
		Dependents = dependents;
	}

	private static decimal? ValidateFinancialAmount(decimal? amount, string propertyName)
	{
		if (amount is null)
		{
			return null;
		}

		if (amount < 0)
		{
			throw new ArgumentOutOfRangeException(propertyName,
					$"{propertyName} cannot be negative. Value: {amount}");
		}

		if (amount > UserConstants.MaxFinancialAmount)
		{
			throw new ArgumentOutOfRangeException(propertyName,
					$"{propertyName} cannot exceed {UserConstants.MaxFinancialAmount:C}. Value: {amount}");
		}

		return amount;
	}

	private static int? ValidateAge(int? age)
	{
		if (age is null)
		{
			return null;
		}

		if (age is < UserConstants.MinAge or > UserConstants.MaxAge)
		{
			throw new ArgumentOutOfRangeException(nameof(age),
					$"Age must be between {UserConstants.MinAge} and {UserConstants.MaxAge}. Value: {age}");
		}

		return age;
	}

	private static int? ValidateDependents(int? dependents)
	{
		if (dependents is null)
		{
			return null;
		}

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

		return dependents;
	}

}