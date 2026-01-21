namespace LoanHub.Aggregator.Web.Endpoints.Offers.Get;

public class GetOfferValidator : Validator<GetOfferRequest>
{
	public GetOfferValidator()
	{
		RuleFor(request => request.OfferId).
			GreaterThanOrEqualTo(0);
		RuleFor(request => request.ProviderType).
			NotNull();
	}
}