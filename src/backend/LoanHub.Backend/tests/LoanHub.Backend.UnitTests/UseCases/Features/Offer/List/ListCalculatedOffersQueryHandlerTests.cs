namespace LoanHub.Backend.UnitTests.UseCases.Features.Offer.List;

public class ListCalculatedOffersQueryHandlerTests
{
	private readonly IOfferCalculator _offerCalculator;
	private readonly IReadRepository<OfferEntity> _offersRepository;
	private readonly IMapper _mapper;
	private readonly ListCalculatedOffersQueryHandler _handler;

	public ListCalculatedOffersQueryHandlerTests()
	{
		_offerCalculator = Substitute.For<IOfferCalculator>();
		_offersRepository = Substitute.For<IReadRepository<OfferEntity>>();
		_mapper = Substitute.For<IMapper>();
		_handler = new ListCalculatedOffersQueryHandler(_offerCalculator, _offersRepository, _mapper);
	}

	[Fact]
	public async Task Handle_ShouldReturnSuccess_WithFilteredCalculatedOffers()
	{
		// Arrange
		var query = new ListCalculatedOffersQuery(1000, 12, 5000, 1000, 30, 2);
		var offer = new OfferEntity("Offer1", "Desc1", new AmountRange(500, 1500), new DurationRange(6, 24), new InterestRateRange(5, 10), new ValidRange(DateTime.UtcNow, DateTime.UtcNow.AddDays(30)));
		var offers = new List<OfferEntity> { offer };
		var conditions = new OfferConditionsDto(1000, 12, 7.5m);
		var dto = new CalculatedOfferDto(1, "Offer1", "Desc1", 1000, 12, 7.5m, DateTime.UtcNow, DateTime.UtcNow.AddDays(30));

		_offersRepository.ListAsync(Arg.Any<OffersByAmountAndDurationSpec>(), Arg.Any<CancellationToken>())
			.Returns(offers);
		_offerCalculator.Calculate(offer, 5000, 1000, 30, 2)
			.Returns(conditions);
		_mapper.Map<CalculatedOfferDto>((offer, conditions))
			.Returns(dto);

		// Act
		var result = await _handler.Handle(query, CancellationToken.None);

		// Assert
		result.IsSuccess.ShouldBeTrue();
		result.Value.ShouldContain(dto);
		await _offersRepository.Received(1).ListAsync(Arg.Is<OffersByAmountAndDurationSpec>(s => s != null), Arg.Any<CancellationToken>());
		_offerCalculator.Received(1).Calculate(offer, 5000, 1000, 30, 2);
		_mapper.Received(1).Map<CalculatedOfferDto>((offer, conditions));
	}

	[Fact]
	public async Task Handle_ShouldFilterOutOffers_WhereCalculatedAmountLessThanRequested()
	{
		// Arrange
		var query = new ListCalculatedOffersQuery(1000, 12, 5000, 1000, 30, 2);
		var offer = new OfferEntity("Offer1", "Desc1", new AmountRange(500, 1500), new DurationRange(6, 24), new InterestRateRange(5, 10), new ValidRange(DateTime.UtcNow, DateTime.UtcNow.AddDays(30)));
		var offers = new List<OfferEntity> { offer };
		var conditions = new OfferConditionsDto(800, 12, 7.5m); // Amount less than requested
		var calculatedOffer = new CalculatedOfferDto(
			0,
			"Offer1",
			"Desc1",
			800,
			12,
			7.5m,
			DateTime.UtcNow,
			DateTime.UtcNow.AddDays(30));

		_mapper.Map<CalculatedOfferDto>((offer, conditions)).
			Returns(calculatedOffer);

		_offersRepository.ListAsync(Arg.Any<OffersByAmountAndDurationSpec>(), Arg.Any<CancellationToken>())
			.Returns(offers);
		_offerCalculator.Calculate(offer, 5000, 1000, 30, 2)
			.Returns(conditions);

		// Act
		var result = await _handler.Handle(query, CancellationToken.None);

		// Assert
		result.IsSuccess.ShouldBeTrue();
		result.Value.ShouldBeEmpty();
	}

	[Fact]
	public async Task Handle_ShouldFilterOutOffers_WhereCalculatedDurationLessThanRequested()
	{
		// Arrange
		var query = new ListCalculatedOffersQuery(1000, 12, 5000, 1000, 30, 2);

		var offer = new OfferEntity("Offer1",
			"Desc1",
			new AmountRange(500,
				1500),
			new DurationRange(6,
				24),
			new InterestRateRange(5,
				10),
			new ValidRange(DateTime.UtcNow,
				DateTime.UtcNow.AddDays(30)));

		var offers = new List<OfferEntity> { offer };
		var conditions = new OfferConditionsDto(1000, 10, 7.5m); // Duration less than requested

		var calculatedOffer = new CalculatedOfferDto(0,
			"Offer1",
			"Desc1",
			1000,
			10,
			7.5m,
			DateTime.UtcNow,
			DateTime.UtcNow.AddDays(30));

		_mapper.Map<CalculatedOfferDto>((offer, conditions)).Returns(calculatedOffer);
		_offersRepository.ListAsync(Arg.Any<OffersByAmountAndDurationSpec>(), Arg.Any<CancellationToken>())
			.Returns(offers);
		_offerCalculator.Calculate(offer, 5000, 1000, 30, 2)
			.Returns(conditions);

		// Act
		var result = await _handler.Handle(query, CancellationToken.None);

		// Assert
		result.IsSuccess.ShouldBe(true);
		result.Value.ShouldBeEmpty();
	}

	[Fact]
	public async Task Handle_ShouldReturnEmptyList_WhenNoOffersMatchSpec()
	{
		// Arrange
		var query = new ListCalculatedOffersQuery(1000, 12, 5000, 1000, 30, 2);
		var offers = new List<OfferEntity>();

		_offersRepository.ListAsync(Arg.Any<OffersByAmountAndDurationSpec>(), Arg.Any<CancellationToken>())
			.Returns(offers);

		// Act
		var result = await _handler.Handle(query, CancellationToken.None);

		// Assert
		result.IsSuccess.ShouldBeTrue();
		result.Value.ShouldBeEmpty();
	}

	[Fact]
	public async Task Handle_ShouldPassCancellationToken()
	{
		// Arrange
		var query = new ListCalculatedOffersQuery(1000, 12, 5000, 1000, 30, 2);
		var cts = new CancellationTokenSource();
		var offers = new List<OfferEntity>();

		_offersRepository.ListAsync(Arg.Any<OffersByAmountAndDurationSpec>(), cts.Token)
			.Returns(offers);

		// Act
		await _handler.Handle(query, cts.Token);

		// Assert
		await _offersRepository.Received(1).ListAsync(Arg.Any<OffersByAmountAndDurationSpec>(), cts.Token);
	}
}