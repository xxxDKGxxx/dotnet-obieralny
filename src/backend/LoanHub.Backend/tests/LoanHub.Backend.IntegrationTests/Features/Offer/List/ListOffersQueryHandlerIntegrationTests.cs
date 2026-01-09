using LoanHub.Backend.IntegrationTests.Data;

namespace LoanHub.Backend.IntegrationTests.Features.Offer.List;

public class ListOffersQueryHandlerIntegrationTests : BaseEfRepoTestFixture
{
	private readonly EfRepository<OfferEntity> _offerRepository;
	private readonly IMapper _mapper;
	private readonly ListOffersQueryHandler _handler;

	public ListOffersQueryHandlerIntegrationTests()
	{
		_offerRepository = new EfRepository<OfferEntity>(_dbContext);

		var config = new MapperConfiguration(
			cfg =>
			{
				cfg.AddProfile<OfferProfile>();
			},
			new SerilogLoggerFactory());

		_mapper = config.CreateMapper();
		_handler = new ListOffersQueryHandler(_offerRepository, _mapper);
	}

	[Fact]
	public async Task Handle_ShouldReturnOffersMatchingSpec()
	{
		// Arrange
		var offer1 = new OfferEntity("Offer1", "Desc1", new AmountRange(500, 1500), new DurationRange(6, 24), new InterestRateRange(5, 10), new ValidRange(DateTime.UtcNow, DateTime.UtcNow.AddDays(30)));
		var offer2 = new OfferEntity("Offer2", "Desc2", new AmountRange(2000, 5000), new DurationRange(12, 36), new InterestRateRange(6, 12), new ValidRange(DateTime.UtcNow, DateTime.UtcNow.AddDays(30)));

		await _dbContext.Set<OfferEntity>()
			.AddRangeAsync(offer1, offer2);

		await _dbContext.SaveChangesAsync();

		var query = new ListOffersQuery(1000, 12);

		// Act
		var result = await _handler.Handle(query, CancellationToken.None);

		// Assert
		result.IsSuccess.ShouldBeTrue();
		result.Value.ShouldContain(dto => dto.Title == "Offer1");
		result.Value.ShouldNotContain(dto => dto.Title == "Offer2");
	}

	[Fact]
	public async Task Handle_ShouldReturnEmpty_WhenNoMatches()
	{
		// Arrange
		var offer = new OfferEntity("Offer1", "Desc1", new AmountRange(2000, 5000), new DurationRange(12, 36), new InterestRateRange(5, 10), new ValidRange(DateTime.UtcNow, DateTime.UtcNow.AddDays(30)));
		await _dbContext.Set<OfferEntity>().AddAsync(offer);
		await _dbContext.SaveChangesAsync();

		var query = new ListOffersQuery(1000, 12); // Doesn't match

		// Act
		var result = await _handler.Handle(query, CancellationToken.None);

		// Assert
		result.IsSuccess.ShouldBeTrue();
		result.Value.ShouldBeEmpty();
	}
}