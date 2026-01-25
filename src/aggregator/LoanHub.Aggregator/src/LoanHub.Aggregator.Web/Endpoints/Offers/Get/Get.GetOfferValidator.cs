namespace LoanHub.Aggregator.Web.Endpoints.Offers.Get;

public class GetOfferValidator : Validator<GetOfferRequest>
{
	public GetOfferValidator()
	{
		RuleFor(request => request.OfferId).
			GreaterThanOrEqualTo(0);
		RuleFor(request => request.ProviderType).
			Custom((providerType, context) =>
			{
				try
				{
					ApplicationProviderType.FromValue(providerType);
				}
				catch (KeyNotFoundException)
				{
					context.AddFailure(
						nameof(UpdateApplicationStatusRequest.NewStatus),
						"Not a valid ProviderType");
				}
			});
	}
}