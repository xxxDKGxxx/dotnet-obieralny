namespace LoanHub.Aggregator.Web.Endpoints.Offers.Get;

public class GetCalculated(IEnumerable<IOfferProvider> offerProviders) : Endpoint<GetCalculatedOfferRequest, CalculatedOfferWithProviderTypeDto>
{
	public override void Configure()
	{
		AllowAnonymous();
		Get("/calculated-offers/{OfferId:int}");
	}

	public override async Task HandleAsync(GetCalculatedOfferRequest req, CancellationToken ct)
	{
		var provider = offerProviders.Single(op =>
		{
			return op.ProviderType == ApplicationProviderType.FromValue(req.ProviderType);
		});
		var calculatedOffer = await provider.GetCalculatedOfferByIdAsync(
			req.OfferId,
			req.Amount,
			req.Duration,
			req.MonthlyIncome,
			req.MonthlyCosts,
			req.Age,
			req.Dependants);

		Response = calculatedOffer;
	}
}