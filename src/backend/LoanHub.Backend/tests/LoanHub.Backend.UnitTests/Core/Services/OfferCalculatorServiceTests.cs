using LoanHub.Backend.Core.Services;

namespace LoanHub.Backend.UnitTests.Core.Services;

public class OfferCalculatorServiceTests
{
	private readonly OfferCalculatorService _sut = new(); // System Under Test

	[Fact]
	public void Calculate_ShouldReturnWorstCaseConditions_WhenOfferIsExpired()
	{
		// Arrange
		var pastDate = DateTime.UtcNow.AddMonths(-2);
		var offer = CreateOffer(validFrom: pastDate.AddDays(-10), validTo: pastDate);

		// Act
		var result = _sut.Calculate(offer, 5000m, 1000m, 30, 0);

		// Assert
		result.ShouldSatisfyAllConditions(
			() =>
			{
				result.amount.ShouldBe(offer.AmountRange.Min);
			},
			() =>
			{
				result.duration.ShouldBe(offer.DurationRange.Min);
			},
			() =>
			{
				result.interestRate.ShouldBe(offer.InterestRateRange.Max) // Worst interest is Max
				;
			});
	}

	[Fact]
	public void Calculate_ShouldReturnWorstCaseConditions_WhenCustomerHasNoDisposableIncome()
	{
		// Arrange
		var offer = CreateOffer();
		const decimal income = 2000m;
		const decimal costs = 2000m; // Income == Costs

		// Act
		var result = _sut.Calculate(offer, income, costs, 30, 0);

		// Assert
		result.ShouldSatisfyAllConditions(
			() =>
			{
				result.amount.ShouldBe(offer.AmountRange.Min);
			},
			() =>
			{
				result.duration.ShouldBe(offer.DurationRange.Min);
			},
			() =>
			{
				result.interestRate.ShouldBe(offer.InterestRateRange.Max);
			});
	}

	[Fact]
	public void Calculate_ShouldReturnBestPossibleConditions_ForPerfectClient()
	{
		// Arrange
		// Klient: Duże zarobki, małe koszty, młody, brak dzieci -> Score bliski 1.0
		var offer = CreateOffer(
			minAmount: 1000, maxAmount: 100000,
			minRate: 5.0m, maxRate: 10.0m,
			minDur: 12, maxDur: 60);

		const decimal income = 20000m;
		const decimal costs = 2000m;
		const int age = 25;
		const int dependants = 0;

		// Act
		var result = _sut.Calculate(offer, income, costs, age, dependants);

		// Assert
		result.ShouldSatisfyAllConditions(
			// Kwota powinna dążyć do Max (interpolacja) i nie być ograniczona zdolnością (bo zarabia dużo)
			() =>
			{
				result.amount.ShouldBeGreaterThan(80000m);
			},
			() =>
			{
				result.amount.ShouldBeLessThanOrEqualTo(offer.AmountRange.Max);
			},

			// Oprocentowanie powinno być bliskie Min
			() =>
			{
				result.interestRate.ShouldBeLessThan(6.0m);
			},
			() =>
			{
				result.interestRate.ShouldBeGreaterThanOrEqualTo(offer.InterestRateRange.Min);
			},

			// Okres kredytowania powinien być bliski Max
			() =>
			{
				result.duration.ShouldBeGreaterThan(50u);
			},
			() =>
			{
				result.duration.ShouldBeLessThanOrEqualTo(offer.DurationRange.Max);
			});
	}

	[Fact]
	public void Calculate_ShouldLimitDuration_WhenClientIsOld()
	{
		// Arrange
		// Klient ma 74 lata. Max wiek to 75. Został mu 1 rok (12 miesięcy) kredytowania.
		// Oferta pozwala na 60 miesięcy.
		var offer = CreateOffer(minDur: 6, maxDur: 60);

		const decimal income = 5000m;
		const decimal costs = 1000m;
		const int age = 74;

		// Act
		var result = _sut.Calculate(offer, income, costs, age, 0);

		// Assert
		// Mimo dobrego scoringu (finanse), wiek musi przyciąć okres kredytowania do max 12 miesięcy
		result.duration.ShouldBeLessThanOrEqualTo((uint)12);
		result.duration.ShouldBeGreaterThanOrEqualTo(offer.DurationRange.Min);
	}

	[Fact]
	public void Calculate_ShouldForceMinimumDuration_WhenAgeRestrictsBelowMinimum()
	{
		// Arrange
		// Klient ma 74.5 roku (zostało 6 miesięcy).
		// Oferta wymaga MINIMUM 12 miesięcy.
		// System powinien zwrócić 12 miesięcy (Clamp do minimum oferty), zamiast 6.
		_ = CreateOffer(minDur: 12, maxDur: 24);
		const int age = 74; // Przyjmujemy pełne lata, w logice jest (75 - 74) * 12 = 12 miesięcy.
							// Żeby test był ciekawszy, załóżmy ofertę min 24 miesiące.
		var strictOffer = CreateOffer(minDur: 24, maxDur: 48);

		// Act
		var result = _sut.Calculate(strictOffer, 5000m, 1000m, age, 0);

		// Assert
		result.duration.ShouldBe(strictOffer.DurationRange.Min);
	}

	[Fact]
	public void Calculate_ShouldClampAmountToCapacity_EvenIfScoreIsHigh()
	{
		// Arrange
		// Klient młody (dobry ageScore), ale zarabia mało (słaby financialScore w wartościach bezwzględnych dla capacity).
		// Scoring może wyjść średni/wysoki, ale matematyczna zdolność (DTI) nie puści dużej kwoty.

		var offer = CreateOffer(minAmount: 1000, maxAmount: 100000);

		// Dochód rozporządzalny: 2500 - 2000 = 500 zł.
		// Max rata (50% DTI) = 250 zł.
		// Przy max racie 250 zł nie da się wziąć 100k kredytu.
		const decimal income = 2500m;
		const decimal costs = 2000m;

		// Act
		var result = _sut.Calculate(offer, income, costs, 25, 0);

		// Assert
		// Sprawdzamy, czy kwota jest drastycznie mniejsza niż Max oferty,
		// wynikająca z matematyki finansowej dla raty 250zł.
		result.amount.ShouldBeLessThan(15000m); // Szacunek z zapasem
		result.amount.ShouldBeGreaterThanOrEqualTo(offer.AmountRange.Min);
	}

	[Fact]
	public void Calculate_ShouldReturnOfferMinimum_WhenCapacityIsZeroButInputsValid()
	{
		// Arrange
		// Sytuacja: Klient ma 1zł dochodu rozporządzalnego. Scoring > 0, ale Capacity bliskie 0.
		// Wynik powinien spaść na AmountRange.Min (zamiast 0 lub błędu).
		var offer = CreateOffer(minAmount: 1000);

		// Act
		// Dochód 2001, koszty 2000 -> 1 zł wolnego.
		var result = _sut.Calculate(offer, 2001m, 2000m, 30, 0);

		// Assert
		result.amount.ShouldBe(offer.AmountRange.Min);
	}

	// --- Helper ---

	private static Offer CreateOffer(
		decimal minAmount = 1000m, decimal maxAmount = 50000m,
		uint minDur = 3, uint maxDur = 36,
		decimal minRate = 5.0m, decimal maxRate = 15.0m,
		DateTime? validFrom = null, DateTime? validTo = null)
	{
		return new Offer(
			"Test Offer",
			"Description",
			new AmountRange(minAmount, maxAmount),
			new DurationRange(minDur, maxDur),
			new InterestRateRange(minRate, maxRate),
			new ValidRange(validFrom ?? DateTime.UtcNow.AddDays(-1), validTo ?? DateTime.UtcNow.AddDays(30))
		);
	}
}