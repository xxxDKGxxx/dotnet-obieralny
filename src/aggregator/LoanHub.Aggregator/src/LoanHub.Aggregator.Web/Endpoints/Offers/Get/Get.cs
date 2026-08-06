namespace LoanHub.Aggregator.Web.Endpoints.Offers.Get;

public class Get(IEnumerable<IOfferProvider> offerProviders) : Endpoint<GetOfferRequest, OfferWithProviderTypeDto>
{
	public override void Configure()
	{
		AllowAnonymous();
		Get("/offers/{OfferId:int}");
	}

	public override async Task HandleAsync(GetOfferRequest req, CancellationToken ct)
	{
		var provider = offerProviders.Single(
			op =>
			{
				return op.ProviderType == ApplicationProviderType.FromValue(req.ProviderType);
			});
		var offer = await provider.GetOfferByIdAsync(req.OfferId, ct);

		Response = offer;
	}
}