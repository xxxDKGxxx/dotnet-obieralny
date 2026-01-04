using LoanHub.Backend.IntegrationTests.Data;

namespace LoanHub.Backend.IntegrationTests.Core.OfferAggregate.Specifications;

public class OffersByAmountAndDurationSpecIntegrationTests : BaseEfRepoTestFixture
{
	private readonly EfRepository<Offer> _offerRepository;

	public OffersByAmountAndDurationSpecIntegrationTests()
	{
		_offerRepository = new EfRepository<Offer>(_dbContext);
	}

	[Fact]
	public async Task Spec_ShouldFilterOffersCorrectly()
	{
		// Arrange
		var offer1 = new Offer("Offer1", "Desc1", new AmountRange(500, 1500), new DurationRange(6, 24), new InterestRateRange(5, 10), new ValidRange(DateTime.UtcNow, DateTime.UtcNow.AddDays(30)));
		var offer2 = new Offer("Offer2", "Desc2", new AmountRange(2000, 5000), new DurationRange(12, 36), new InterestRateRange(6, 12), new ValidRange(DateTime.UtcNow, DateTime.UtcNow.AddDays(30)));
		await _dbContext.Set<Offer>().AddRangeAsync(offer1, offer2);
		await _dbContext.SaveChangesAsync();

		var spec = new OffersByAmountAndDurationSpec(1000, 12);

		// Act
		var results = await _offerRepository.ListAsync(spec, CancellationToken.None);

		// Assert
		results.ShouldContain(offer1);
		results.ShouldNotContain(offer2);
	}

	[Fact]
	public async Task Spec_ShouldReturnEmpty_WhenNoMatches()
	{
		// Arrange
		var offer = new Offer("Offer1", "Desc1", new AmountRange(2000, 5000), new DurationRange(12, 36), new InterestRateRange(5, 10), new ValidRange(DateTime.UtcNow, DateTime.UtcNow.AddDays(30)));
		await _dbContext.Set<Offer>().AddAsync(offer);
		await _dbContext.SaveChangesAsync();

		var spec = new OffersByAmountAndDurationSpec(1000, 12);

		// Act
		var results = await _offerRepository.ListAsync(spec, CancellationToken.None);

		// Assert
		results.ShouldBeEmpty();
	}
}