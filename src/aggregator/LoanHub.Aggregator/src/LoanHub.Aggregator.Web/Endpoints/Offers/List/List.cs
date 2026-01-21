namespace LoanHub.Aggregator.Web.Endpoints.Offers.List;

public class List(IEnumerable<IOfferProvider> offerProviders) :
	Endpoint<ListOffersRequest, IEnumerable<OfferWithProviderTypeDto>>
{
	public override void Configure()
	{
		Get("/offers");
		AllowAnonymous();
	}

	public override async Task HandleAsync(ListOffersRequest req, CancellationToken ct)
	{
		var tasks = offerProviders.Select(async provider =>
		{
			try
			{
				var offers = await provider.ListOffersAsync(req.Amount, req.Duration);
				return offers;
			}
			catch (Exception ex)
			{
				Logger.LogError("{Message}", ex.Message);

				return [];
			}
		});

		var offerCollections = await Task.WhenAll(tasks);
		var result = new List<OfferWithProviderTypeDto>();

		foreach (var offerCollection in offerCollections)
		{
			result.AddRange(offerCollection);
		}

		Response = result;
	}
}