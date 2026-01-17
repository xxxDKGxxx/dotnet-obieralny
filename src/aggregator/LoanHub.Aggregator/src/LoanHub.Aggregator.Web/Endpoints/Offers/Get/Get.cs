using LoanHub.Aggregator.Web.Dtos;

namespace LoanHub.Aggregator.Web.Endpoints.Offers;

public class Get(IEnumerable<IOfferProvider> offerProviders) : Endpoint<GetOfferRequest, OfferWithProviderTypeDto>
{
	public override void Configure()
	{
		AllowAnonymous();
		Get("/offers/{OfferId:int}");
	}

	public override async Task HandleAsync(GetOfferRequest req, CancellationToken ct)
	{
		var headers = this.HttpContext.Request.Headers;

		var provider = offerProviders.Single(op =>
		{
			return op.ProviderType == ApplicationProviderType.FromValue(req.ProviderType);
		});
		var offer = await provider.GetOfferByIdAsync(req.OfferId);

		Response = offer;
	}
}