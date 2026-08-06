namespace LoanHub.Aggregator.Web.Endpoints.Applications.List;

public class List(IEnumerable<IOfferProvider> offerProviders) :
	Endpoint<ListApplicationsRequest, IEnumerable<ApplicationWithProviderTypeDto>>
{
	private IEnumerable<IOfferProvider> _offerProviders = offerProviders;

	public override void Configure()
	{
		Get("/applications");
		Policies("DefaultPolicy");
	}

	public override async Task HandleAsync(ListApplicationsRequest req, CancellationToken ct)
	{
		var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

		if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var requestingUserId))
		{
			await SendUnauthorizedAsync(ct);
			return;
		}

		if (req.ProviderType is not null)
		{
			_offerProviders = _offerProviders.Where(provider =>
			{
				return provider.ProviderType == req.ProviderType;
			});
		}

		var tasks = _offerProviders.Select(async provider =>
		{
			try
			{
				var offers = await provider.ListApplicationsAsync(
					req.UserId,
					requestingUserId,
					ct);

				return offers;
			}
			catch (Exception ex)
			{
				Logger.LogError("{Message}", ex.Message);

				return [];
			}
		});

		var applicationCollections = await Task.WhenAll(tasks);
		var result = new List<ApplicationWithProviderTypeDto>();

		foreach (var offerCollection in applicationCollections)
		{
			result.AddRange(offerCollection);
		}

		Response = result;
	}
}