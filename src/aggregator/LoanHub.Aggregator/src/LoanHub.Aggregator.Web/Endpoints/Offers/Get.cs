using LoanHub.Aggregator.Core.Interfaces;
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
		var provider = offerProviders.Single(op => op.ProviderType == ApplicationProviderType.FromValue(req.ProviderType));
		var offer = await provider.GetOfferByIdAsync(req.OfferId);

		Response = new OfferWithProviderTypeDto(
			offer.Id,
			offer.Title,
			offer.Description,
			offer.MinAmount,
			offer.MaxAmount,
			offer.MinDuration,
			offer.MaxDuration,
			offer.MinInterestRate,
			offer.MaxInterestRate,
			offer.ValidFrom,
			offer.ValidTo,
			provider.ProviderType.Value);
	}
}