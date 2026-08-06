namespace LoanHub.Backend.Web.Endpoints.Offers.Get;

public class GetOfferValidator : Validator<GetOfferRequest>
{
	public GetOfferValidator()
	{
		RuleFor(x => x.OfferId)
			.GreaterThanOrEqualTo(0);
	}
}