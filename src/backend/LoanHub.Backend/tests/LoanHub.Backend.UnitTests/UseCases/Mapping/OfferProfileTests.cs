namespace LoanHub.Backend.UnitTests.UseCases.Mapping;

public class OfferProfileTests
{
	private readonly IMapper _mapper;

	public OfferProfileTests()
	{
		var configuration = new MapperConfiguration(
			cfg =>
			{
				cfg.AddProfile<OfferProfile>();
			},
			new SerilogLoggerFactory());

		_mapper = configuration.CreateMapper();
	}

	[Fact]
	public void Configuration_ShouldBeValid()
	{
		var configuration = new MapperConfiguration(
			cfg =>
			{
				cfg.AddProfile<OfferProfile>();
			},
			new SerilogLoggerFactory());

		configuration.AssertConfigurationIsValid();
	}

	[Fact]
	public void Map_OfferToOfferDto_ShouldMapCorrectly()
	{
		// Arrange
		var offer = new Offer(
			"Test Title",
			"Test Description",
			new AmountRange(1000, 5000),
			new DurationRange(6, 24),
			new InterestRateRange(5.5m, 12.0m),
			new ValidRange(DateTime.UtcNow.AddDays(-1), DateTime.UtcNow.AddDays(30))
		);

		// Act
		var dto = _mapper.Map<OfferDto>(offer);

		// Assert
		dto.ShouldSatisfyAllConditions(
			() =>
			{
				dto.Id.ShouldBe(offer.Id);
			},
			() =>
			{
				dto.Title.ShouldBe(offer.Title);
			},
			() =>
			{
				dto.Description.ShouldBe(offer.Description);
			},
			() =>
			{
				dto.MinAmount.ShouldBe(offer.AmountRange.Min);
			},
			() =>
			{
				dto.MaxAmount.ShouldBe(offer.AmountRange.Max);
			},
			() =>
			{
				dto.MinDuration.ShouldBe(offer.DurationRange.Min);
			},
			() =>
			{
				dto.MaxDuration.ShouldBe(offer.DurationRange.Max);
			},
			() =>
			{
				dto.MinInterestRate.ShouldBe(offer.InterestRateRange.Min);
			},
			() =>
			{
				dto.MaxInterestRate.ShouldBe(offer.InterestRateRange.Max);
			},
			() =>
			{
				dto.ValidFrom.ShouldBe(offer.ValidRange.Min);
			},
			() =>
			{
				dto.ValidTo.ShouldBe(offer.ValidRange.Max);
			});
	}

	[Fact]
	public void Map_OfferAndConditionsToCalculatedOfferDto_ShouldMapCorrectly()
	{
		// Arrange
		var offer = new Offer(
			"Test Title",
			"Test Description",
			new AmountRange(1000, 5000),
			new DurationRange(6, 24),
			new InterestRateRange(5.5m, 12.0m),
			new ValidRange(DateTime.UtcNow.AddDays(-1), DateTime.UtcNow.AddDays(30))
		);
		var conditions = new OfferConditionsDto(2500, 12, 7.5m);

		// Act
		var dto = _mapper.Map<CalculatedOfferDto>((offer, conditions));

		// Assert
		dto.ShouldSatisfyAllConditions(
			() =>
			{
				dto.Id.ShouldBe(offer.Id);
			},
			() =>
			{
				dto.Title.ShouldBe(offer.Title);
			},
			() =>
			{
				dto.Description.ShouldBe(offer.Description);
			},
			() =>
			{
				dto.Amount.ShouldBe(conditions.Amount);
			},
			() =>
			{
				dto.Duration.ShouldBe(conditions.Duration);
			},
			() =>
			{
				dto.InterestRate.ShouldBe(conditions.InterestRate);
			},
			() =>
			{
				dto.ValidFrom.ShouldBe(offer.ValidRange.Min);
			},
			() =>
			{
				dto.ValidTo.ShouldBe(offer.ValidRange.Max);
			});
	}
}