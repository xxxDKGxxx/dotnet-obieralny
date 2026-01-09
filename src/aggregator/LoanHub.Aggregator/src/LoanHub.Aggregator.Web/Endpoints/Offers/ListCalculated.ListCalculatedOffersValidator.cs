using FluentValidation;

namespace LoanHub.Aggregator.Web.Endpoints.Offers;

public sealed class ListCalculatedOffersValidator : Validator<ListCalculatedOffersRequest>
{
	public ListCalculatedOffersValidator()
	{
		RuleFor(request => request.Amount).
			GreaterThan(0);
		RuleFor(request => request.Duration).
			GreaterThan(0u);
		RuleFor(request => request.Age).
			InclusiveBetween(1, 75);
		RuleFor(request => request.Dependants)
			.GreaterThanOrEqualTo(0);
		RuleFor(request => request.MonthlyCosts)
			.GreaterThan(0);
		RuleFor(request => request.MonthlyIncome)
			.GreaterThan(0);
	}
}