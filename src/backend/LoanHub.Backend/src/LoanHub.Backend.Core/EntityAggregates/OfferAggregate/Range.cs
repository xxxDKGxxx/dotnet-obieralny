namespace LoanHub.Backend.Core.EntityAggregates.OfferAggregate;

public class Range<T>(T min, T max) : ValueObject where T : notnull, IComparable<T>
{
	public T Min { get; private set; } = min;
	public T Max { get; private set; } = max;

	protected override IEnumerable<object> GetEqualityComponents()
	{
		yield return Min;
		yield return Max;
	}

	public bool IsInRange(T value)
	{
		return value.CompareTo(Min) >= 0 && value.CompareTo(Max) <= 0;
	}
}