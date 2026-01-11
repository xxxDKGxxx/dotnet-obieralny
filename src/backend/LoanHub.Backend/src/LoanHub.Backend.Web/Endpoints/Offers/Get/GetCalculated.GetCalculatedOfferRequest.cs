namespace LoanHub.Backend.Web.Endpoints.Offers.Get;

public record GetCalculatedOfferRequest(
	int OfferId,
	decimal MonthlyIncome,
	decimal MonthlyCosts,
	int Age,
	int Dependants);