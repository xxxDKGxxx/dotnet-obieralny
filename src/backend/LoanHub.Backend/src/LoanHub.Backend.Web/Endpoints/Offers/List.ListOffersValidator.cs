namespace LoanHub.Backend.Web.Endpoints.Offers;

public sealed class ListOffersValidator : Validator<ListOffersRequest>
{
	public ListOffersValidator()
	{
		RuleFor(x => x.Amount).
			GreaterThan(0);

		RuleFor(x => x.Duration).
			GreaterThan(0u);
	}
}