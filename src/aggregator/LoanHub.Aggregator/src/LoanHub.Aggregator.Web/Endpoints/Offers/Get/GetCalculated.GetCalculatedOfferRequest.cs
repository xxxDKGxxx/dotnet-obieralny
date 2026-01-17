namespace LoanHub.Aggregator.Web.Endpoints.Offers;

public record GetCalculatedOfferRequest(
	int OfferId,
	decimal Amount,
	uint Duration,
	decimal MonthlyIncome,
	decimal MonthlyCosts,
	int Age,
	int Dependants,
	string ProviderType);