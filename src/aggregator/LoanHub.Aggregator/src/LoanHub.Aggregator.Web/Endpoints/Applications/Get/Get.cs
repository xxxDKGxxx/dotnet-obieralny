namespace LoanHub.Aggregator.Web.Endpoints.Applications.Get;

public class Get(IEnumerable<IOfferProvider> offerProviders) :
	Endpoint<GetApplicationByIdRequest, ApplicationWithProviderTypeDto>
{
	public override void Configure()
	{
		Get("/applications/{ApplicationId:int}");
		AllowAnonymous();
	}

	public override async Task HandleAsync(GetApplicationByIdRequest req, CancellationToken ct)
	{
		var provider = offerProviders.Single(op =>
		{
			return op.ProviderType == ApplicationProviderType.FromValue(req.ProviderType);
		});
		var application = await provider.GetApplicationByIdAsync(req.ApplicationId);

		Response = application;
	}
}