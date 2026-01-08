using LoanHub.Backend.Core.EntityAggregates.OfferAggregate;
using LoanHub.Backend.Core.EntityAggregates.OfferAggregate.Specifications;

namespace LoanHub.Backend.UnitTests.Core.OfferAggregate.Specifications;

public class OffersByAmountAndDurationSpecTests
{
	[Theory]
	[InlineData(1000, 12, true)]
	[InlineData(500, 12, false)]
	[InlineData(1500, 12, false)]
	[InlineData(1000, 5, false)]
	[InlineData(1000, 25, false)]
	[InlineData(750, 6, true)]
	[InlineData(1250, 18, true)]
	public void Matches_ShouldReturnExpectedResult(decimal amount, uint duration, bool expected)
	{
		// Arrange
		var offer = CreateOffer(750, 1250, 6, 18);
		var spec = new OffersByAmountAndDurationSpec(amount, duration);

		// Act
		var result = spec.Evaluate([offer]).Any();

		// Assert
		result.ShouldBe(expected);
	}

	private static Offer CreateOffer(decimal minAmount, decimal maxAmount, uint minDuration, uint maxDuration)
	{
		return new Offer(
			"Test",
			"Desc",
			new AmountRange(minAmount, maxAmount),
			new DurationRange(minDuration, maxDuration),
			new InterestRateRange(5, 10),
			new ValidRange(DateTime.UtcNow.AddDays(-1), DateTime.UtcNow.AddDays(1))
		);
	}
}