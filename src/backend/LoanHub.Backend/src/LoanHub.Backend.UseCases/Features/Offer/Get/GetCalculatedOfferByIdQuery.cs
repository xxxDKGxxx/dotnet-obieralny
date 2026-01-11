namespace LoanHub.Backend.UseCases.Features.Offer.Get;

public sealed record GetCalculatedOfferByIdQuery(
	int OfferId,
	decimal MonthlyIncome,
	decimal MonthlyCosts,
	int Age,
	int Dependants) : IQuery<Result<CalculatedOfferDto>>;