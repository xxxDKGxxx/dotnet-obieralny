namespace LoanHub.Backend.Core.EntityAggregates.OfferAggregate;

public sealed class OfferAggregate : LoanHubEntityBase, IAggregateRoot
{

	public int EmployeeId { get; private set; } // TODO Link it to User table when it is created

	public string Title { get; private set;}

	public string Description { get; private set; }
	public AmountRange AmountRange { get; private set; }
	public InterestRateRange InterestRateRange { get; private set; }
	public ValidRange ValidRange { get; private set; }
}

public sealed class AmountRange(decimal min, decimal max) : ValueObject
{
	public decimal Min { get; private set; } = min;

	public decimal Max { get; private set; } = max;

	protected override IEnumerable<object> GetEqualityComponents()
	{
		yield return Min;
		yield return Max;
	}
}

public sealed class InterestRateRange(decimal min, decimal max) : ValueObject
{
	public decimal Min { get; private set; } = min;
	public decimal Max { get; private set; } = max;

	protected override IEnumerable<object> GetEqualityComponents()
	{
		yield return Min;
		yield return Max;
	}
}

public sealed class ValidRange(decimal min, decimal max) : ValueObject
{
	public decimal Min { get; private set; } = min;
	public decimal Max { get; private set; } = max;

	protected override IEnumerable<object> GetEqualityComponents()
	{
		yield return Min;
		yield return Max;
	}
}