namespace LoanHub.Aggregator.Web.Endpoints.Documents.Get;

public class Get(IEnumerable<IOfferProvider> offerProviders) : Endpoint<GetDocumentRequest>
{
	public override void Configure()
	{
		Get("/documents/{documentId}");
		Policies("DefaultPolicy");
	}

	public override async Task HandleAsync(GetDocumentRequest req, CancellationToken ct)
	{
		var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

		if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var requestingUserId))
		{
			await SendUnauthorizedAsync(ct);
			return;
		}

		var offerProvider = offerProviders.Single(
			op =>
			{
				return op.ProviderType == ApplicationProviderType.FromValue(req.ProviderType);
			});

		var result = await offerProvider.DownloadDocumentAsync(
			req.DocumentId,
			req.ApplicationId,
			requestingUserId,
			ct);

		await SendStreamAsync(
			result.Content,
			result.FileName,
			contentType: result.ContentType,
			cancellation: ct);
	}
}