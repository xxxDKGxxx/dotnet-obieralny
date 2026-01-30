namespace LoanHub.Aggregator.Web.Endpoints.Documents.Get;

public class Get(IEnumerable<IOfferProvider> offerProviders) : Endpoint<GetDocumentRequest>
{
	public override void Configure()
	{
		AllowAnonymous();
		Get("/documents/{documentId}");
	}

	public override async Task HandleAsync(GetDocumentRequest req, CancellationToken ct)
	{
		var offerProvider = offerProviders.Single(
			op =>
			{
				return op.ProviderType == ApplicationProviderType.FromValue(req.ProviderType);
			});

		var result = await offerProvider.DownloadDocumentAsync(
			req.DocumentId,
			req.ApplicationId,
			ct);

		await SendStreamAsync(
			result.Content,
			result.FileName,
			contentType: result.ContentType,
			cancellation: ct);
	}
}