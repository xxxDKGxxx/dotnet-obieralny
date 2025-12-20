namespace LoanHub.Backend.Core.EntityAggregates.OfferAggregate;

public sealed class Offer(
	string title,
	string description,
	AmountRange amountRange,
	DurationRange durationRange,
	InterestRateRange interestRateRange,
	ValidRange validRange):
	LoanHubEntityBase,
	IAggregateRoot
{
	public string Title { get; private set; } = title;
	public string Description { get; private set; } = description;
	public AmountRange AmountRange { get; private set; } = amountRange;
	public DurationRange DurationRange { get; private set; } = durationRange;
	public InterestRateRange InterestRateRange { get; private set; } = interestRateRange;
	public ValidRange ValidRange { get; private set; } = validRange;
}

public sealed class AmountRange : Range<decimal>
{
	public AmountRange(decimal min, decimal max) : base(min, max)
	{
		if (min > max)
		{
			throw new ArgumentException("Min value must be less than max value");
		}

		if (min <= 0)
		{
			throw new ArgumentException("Min value must be greater than 0");
		}

		if (max <= 0)
		{
			throw new ArgumentException("Max value must be greater than 0");
		}
	}
}

public sealed class DurationRange : Range<uint>
{
	public DurationRange(uint min, uint max) : base(min, max)
	{
		if (min > max)
		{
			throw new ArgumentException("Min value must be less than max value");
		}
	}
}

public sealed class InterestRateRange : Range<decimal>
{
	public InterestRateRange(decimal min, decimal max) : base(min, max)
	{
		if (min > max)
		{
			throw new ArgumentException("Min value must be less than max value");
		}

		if (min <= 0)
		{
			throw new ArgumentException("Min value must be greater than 0");
		}

		if (max <= 0)
		{
			throw new ArgumentException("Max value must be greater than 0");
		}
	}
}

public sealed class ValidRange : Range<DateTime>
{
	public ValidRange(DateTime min, DateTime max) : base(min, max)
	{
		if (min > max)
		{
			throw new ArgumentException("Min value must be less than max value");
		}
	}
}