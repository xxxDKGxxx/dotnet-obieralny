namespace LoanHub.Backend.UnitTests.UseCases.Features.Offer.List;

public class ListOffersQueryHandlerTests
{
	private readonly IReadRepository<OfferEntity> _offerRepository;
	private readonly IMapper _mapper;
	private readonly ListOffersQueryHandler _handler;

	public ListOffersQueryHandlerTests()
	{
		_offerRepository = Substitute.For<IReadRepository<OfferEntity>>();
		_mapper = Substitute.For<IMapper>();
		_handler = new ListOffersQueryHandler(_offerRepository, _mapper);
	}

	[Fact]
	public async Task Handle_ShouldReturnSuccess_WithMappedOffers()
	{
		// Arrange
		var query = new ListOffersQuery(1000, 12);
		var offers = new List<OfferEntity>
		{
			new("Offer1", "Desc1", new AmountRange(500, 1500), new DurationRange(6, 24), new InterestRateRange(5, 10), new ValidRange(DateTime.UtcNow, DateTime.UtcNow.AddDays(30)))
		};
		var dtos = new List<OfferDto>
		{
			new(1, "Offer1", "Desc1", 500, 1500, 6, 24, 5, 10, DateTime.UtcNow, DateTime.UtcNow.AddDays(30))
		};

		_offerRepository.ListAsync(Arg.Any<OffersByAmountAndDurationSpec>(), Arg.Any<CancellationToken>())
			.Returns(offers);
		_mapper.Map<OfferDto>(Arg.Any<OfferEntity>())
			.Returns(dtos.First());

		// Act
		var result = await _handler.Handle(query, CancellationToken.None);

		// Assert
		result.IsSuccess.ShouldBeTrue();

		result.Value.ToList()
			.ShouldBeEquivalentTo(dtos);

		await _offerRepository.Received(1).ListAsync(Arg.Is<OffersByAmountAndDurationSpec>(s => s != null), Arg.Any<CancellationToken>());
		_mapper.Received(1).Map<OfferDto>(offers[0]);
	}

	[Fact]
	public async Task Handle_ShouldReturnEmptyList_WhenNoOffersMatch()
	{
		// Arrange
		var query = new ListOffersQuery(1000, 12);
		var offers = new List<OfferEntity>();

		_offerRepository.ListAsync(Arg.Any<OffersByAmountAndDurationSpec>(), Arg.Any<CancellationToken>())
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
		var query = new ListOffersQuery(1000, 12);
		var cts = new CancellationTokenSource();
		var offers = new List<OfferEntity>();

		_offerRepository.ListAsync(Arg.Any<OffersByAmountAndDurationSpec>(), cts.Token)
			.Returns(offers);

		// Act
		await _handler.Handle(query, cts.Token);

		// Assert
		await _offerRepository.Received(1).ListAsync(Arg.Any<OffersByAmountAndDurationSpec>(), cts.Token);
	}
}