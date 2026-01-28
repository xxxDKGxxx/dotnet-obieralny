namespace LoanHub.Aggregator.Web.Endpoints.Applications.Post;

public sealed class Post(IEnumerable<IOfferProvider> offerProviders, ICounterRepository counterRepository) :
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
			op =>
			{
				return op.ProviderType == ApplicationProviderType.FromValue(req.ProviderType);
			});
		;

		var result = await offerProvider.CreateApplicationAsync(
			req.OfferId,
			req.UserId,
			req.Amount,
			req.Duration,
			req.Financials,
			req.Contact,
			req.PersonalData,
			ct);

		await counterRepository.IncrementAtomicAsync(ct);

		Response = result;
	}
}