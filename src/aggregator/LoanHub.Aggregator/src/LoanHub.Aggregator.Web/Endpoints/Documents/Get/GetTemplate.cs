namespace LoanHub.Aggregator.Web.Endpoints.Documents.Get;

public class GetTemplate(IEnumerable<IOfferProvider> offerProviders) : Endpoint<GetTemplateRequest>
{
	public override void Configure()
	{
		AllowAnonymous();
		Get("/documents/template");
	}

	public override async Task HandleAsync(GetTemplateRequest req, CancellationToken ct)
	{
		var offerProvider =
			offerProviders.Single(op =>
			{
				return op.ProviderType == ApplicationProviderType.FromValue(req.ProviderType);
			});

		var result = await offerProvider.DownloadDocumentTemplateAsync(ct);

		await SendStreamAsync(result.Content, result.FileName, contentType: result.ContentType, cancellation: ct);
	}
}