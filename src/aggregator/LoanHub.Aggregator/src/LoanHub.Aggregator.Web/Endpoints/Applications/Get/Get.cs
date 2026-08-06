namespace LoanHub.Aggregator.Web.Endpoints.Applications.Get;

public class Get(IEnumerable<IOfferProvider> offerProviders) :
	Endpoint<GetApplicationByIdRequest, ApplicationWithProviderTypeDto>
{
	public override void Configure()
	{
		Get("/applications/{ApplicationId:int}");
		Policies("DefaultPolicy");
	}

	public override async Task HandleAsync(GetApplicationByIdRequest req, CancellationToken ct)
	{
		var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

		if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var requestingUserId))
		{
			await SendUnauthorizedAsync(ct);
			return;
		}

		var provider = offerProviders.Single(op =>
		{
			return op.ProviderType == ApplicationProviderType.FromValue(req.ProviderType);
		});

		var application = await provider.GetApplicationByIdAsync(req.ApplicationId, requestingUserId, ct);

		Response = application;
	}
}