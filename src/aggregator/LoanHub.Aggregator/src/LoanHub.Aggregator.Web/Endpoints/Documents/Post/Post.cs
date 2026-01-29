namespace LoanHub.Aggregator.Web.Endpoints.Documents.Post;

public class Post(IEnumerable<IOfferProvider> offerProviders) : Endpoint<UploadDocumentRequest>
{
	public override void Configure()
	{
		AllowAnonymous();
		AllowFileUploads();
		Post("/documents");
	}

	public override async Task HandleAsync(UploadDocumentRequest req, CancellationToken ct)
	{
		var offerProvider = offerProviders.Single(
			op =>
			{
				return op.ProviderType == ApplicationProviderType.FromValue(req.ProviderType);
			});

		await offerProvider.UploadDocumentAsync(
			req.Document.OpenReadStream(),
			req.Document.ContentType,
			req.ApplicationId,
			req.DocumentId,
			req.Document.FileName,
			ct);
	}
}