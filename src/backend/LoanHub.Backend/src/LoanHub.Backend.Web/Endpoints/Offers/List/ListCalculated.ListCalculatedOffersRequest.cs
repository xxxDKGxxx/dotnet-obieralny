namespace LoanHub.Backend.Web.Endpoints.Offers.List;

public sealed record ListCalculatedOffersRequest(
	decimal Amount,
	uint Duration,
	decimal MonthlyIncome,
	decimal MonthlyCosts,
	int Age,
	int Dependants
);