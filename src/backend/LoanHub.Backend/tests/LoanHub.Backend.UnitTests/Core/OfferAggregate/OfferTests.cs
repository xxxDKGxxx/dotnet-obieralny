namespace LoanHub.Backend.UnitTests.Core.OfferAggregate;

public class OfferTests
{
	[Fact]
	public void Constructor_ShouldInitializeAggregate_Correctly()
	{
		var title = "Super Pożyczka";
		var description = "Najlepsza oferta na rynku";

		var amountRange = new AmountRange(1000m, 50000m);
		var durationRange = new DurationRange(3, 24);
		var interestRateRange = new InterestRateRange(5.5m, 12.0m);
		var validRange = new ValidRange(DateTime.UtcNow, DateTime.UtcNow.AddMonths(1));

		var offer = new Offer(
			title,
			description,
			amountRange,
			durationRange,
			interestRateRange,
			validRange
		);

		offer.ShouldSatisfyAllConditions(
			() =>
			{
				offer.Title.ShouldBe(title);
			},
			() =>
			{
				offer.Description.ShouldBe(description);
			},
			() =>
			{
				offer.AmountRange.ShouldBe(amountRange);
			},
			() =>
			{
				offer.DurationRange.ShouldBe(durationRange);
			},
			() =>
			{
				offer.InterestRateRange.ShouldBe(interestRateRange);
			},
			() =>
			{
				offer.ValidRange.ShouldBe(validRange);
			},
			() =>
			{
				offer.Id.ShouldBe(0);
			},
			() =>
			{
				offer.CreatedAt.ShouldBeInRange(DateTime.UtcNow.AddSeconds(-1), DateTime.UtcNow.AddSeconds(1));
			},
			() =>
			{
				offer.IsDeleted.ShouldBeFalse();
			});
	}
}