namespace LoanHub.Aggregator.Web.Endpoints.Offers.Get;

public class GetCalculatedOfferValidator : Validator<GetCalculatedOfferRequest>
{
	public GetCalculatedOfferValidator()
	{
		RuleFor(o => o.OfferId).
			GreaterThanOrEqualTo(0);
		RuleFor(o => o.Amount).
			GreaterThan(0);
		RuleFor(o => o.Duration).
			GreaterThan(0u);
		RuleFor(o => o.MonthlyIncome)
			.GreaterThanOrEqualTo(o => o.MonthlyCosts);
		RuleFor(o => o.MonthlyCosts)
			.GreaterThanOrEqualTo(0);
		RuleFor(o => o.Age).
			InclusiveBetween(1, 75);
		RuleFor(o => o.Dependants)
			.GreaterThanOrEqualTo(0);
		RuleFor(o => o.ProviderType).
			NotNull();
	}
}