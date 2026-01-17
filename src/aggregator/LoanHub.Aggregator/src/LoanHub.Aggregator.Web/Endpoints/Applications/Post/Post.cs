namespace LoanHub.Aggregator.Web.Endpoints.Applications.Post;

public sealed class Post(IEnumerable<IOfferProvider> offerProviders) :
	Endpoint<PostApplicationRequest, ApplicationWithProviderTypeDto>
{
	public override void Configure()
	{
		AllowAnonymous();
		Post("/applications");
	}

	public override async Task HandleAsync(PostApplicationRequest req, CancellationToken ct)
	{
		var offerProvider = offerProviders.Single(
			op => op.ProviderType == ApplicationProviderType.FromValue(req.ProviderType));;

		var result = await offerProvider.CreateApplicationAsync(
			req.OfferId,
			req.UserId,
			req.Amount,
			req.Duration,
			req.Financials,
			req.Contact,
			req.PersonalData);

		Response = result;
	}
}