using FluentValidation;

namespace LoanHub.Aggregator.Web.Endpoints.Offers;

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