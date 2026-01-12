using LoanHub.Backend.IntegrationTests.Data;

namespace LoanHub.Backend.IntegrationTests.Features.Offer.List;

public class ListCalculatedOffersQueryHandlerIntegrationTests : BaseEfRepoTestFixture
{
	private readonly IOfferCalculator _offerCalculator;
	private readonly IReadRepository<OfferEntity> _offersRepository;
	private readonly IMapper _mapper;
	private readonly ListCalculatedOffersQueryHandler _handler;

	public ListCalculatedOffersQueryHandlerIntegrationTests()
	{
		_offerCalculator = Substitute.For<IOfferCalculator>();
		_offersRepository = new EfRepository<OfferEntity>(_dbContext);

		var config = new MapperConfiguration(
			cfg =>
			{
				cfg.AddProfile<OfferProfile>();
			},
			new SerilogLoggerFactory());

		_mapper = config.CreateMapper();
		_handler = new ListCalculatedOffersQueryHandler(_offerCalculator, _offersRepository, _mapper);
	}

	[Fact]
	public async Task Handle_ShouldReturnFilteredCalculatedOffers()
	{
		// Arrange
		var offer = new OfferEntity(
			"Offer1",
			"Desc1",
			new AmountRange(500, 1500),
			new DurationRange(6, 24),
			new InterestRateRange(5, 10),
			new ValidRange(DateTime.UtcNow, DateTime.UtcNow.AddDays(30)));

		await _dbContext.Set<OfferEntity>().AddAsync(offer);
		await _dbContext.SaveChangesAsync();

		var query = new ListCalculatedOffersQuery(1000, 12, 5000, 1000, 30, 2);
		var conditions = new OfferConditionsDto(1000, 12, 7.5m);
		var dto = new CalculatedOfferDto(offer.Id, "Offer1", "Desc1", 1000, 12, 7.5m, offer.ValidRange.Min, offer.ValidRange.Max);

		_offerCalculator.Calculate(offer, 1000, 12, 5000, 1000, 30, 2)
			.Returns(conditions);

		// Act
		var result = await _handler.Handle(query, CancellationToken.None);

		// Assert
		result.IsSuccess.ShouldBeTrue();
		result.Value.ShouldContain(dto);
	}

	[Fact]
	public async Task Handle_ShouldFilterOut_WhenCalculatedDoesNotMatch()
	{
		// Arrange
		var offer = new OfferEntity("Offer1", "Desc1", new AmountRange(500, 1500), new DurationRange(6, 24), new InterestRateRange(5, 10), new ValidRange(DateTime.UtcNow, DateTime.UtcNow.AddDays(30)));
		await _dbContext.Set<OfferEntity>().AddAsync(offer);
		await _dbContext.SaveChangesAsync();

		var query = new ListCalculatedOffersQuery(1000, 12, 5000, 1000, 30, 2);
		var conditions = new OfferConditionsDto(800, 12, 7.5m); // Amount less
		var dto = new CalculatedOfferDto(offer.Id, "Offer1", "Desc1", 800, 12, 7.5m, offer.ValidRange.Min, offer.ValidRange.Max);

		_offerCalculator.Calculate(offer, 1000, 12, 5000, 1000, 30, 2)
			.Returns(conditions);

		// Act
		var result = await _handler.Handle(query, CancellationToken.None);

		// Assert
		result.IsSuccess.ShouldBeTrue();
		result.Value.ShouldContain(dto);
	}
}