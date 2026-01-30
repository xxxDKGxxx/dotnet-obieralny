using LoanHub.Backend.UseCases.Features.Documents.Download;

namespace LoanHub.Backend.Web.Endpoints.Documents.Get;

public class Get(IMediator mediator) : Endpoint<GetDocumentRequest>
{
	public override void Configure()
	{
		Version(1);
		Get("/documents/{documentId}");
	}

	public override async Task HandleAsync(GetDocumentRequest req, CancellationToken ct)
	{
		var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

		if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
		{
			await SendUnauthorizedAsync(ct);
			return;
		}

		var query = new DownloadDocumentQuery(userId, req.ApplicationId, req.DocumentId);
		var result = await mediator.Send(query, ct);

		if (result.IsSuccess)
		{
			await SendStreamAsync(
				result.Value.Content,
				result.Value.FileName,
				contentType: result.Value.ContentType,
				cancellation: ct);
			return;
		}

		await result.SendResult(this, ct);
	}
}