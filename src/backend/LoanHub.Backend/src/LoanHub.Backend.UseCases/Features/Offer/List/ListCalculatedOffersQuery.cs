namespace LoanHub.Backend.UseCases.Features.Offer.List;

public sealed record ListCalculatedOffersQuery(
	decimal Amount,
	uint Duration,
	decimal MonthlyIncome,
	decimal MonthlyCosts,
	int Age,
	int Dependants) : IQuery<Result<IEnumerable<CalculatedOfferDto>>>;