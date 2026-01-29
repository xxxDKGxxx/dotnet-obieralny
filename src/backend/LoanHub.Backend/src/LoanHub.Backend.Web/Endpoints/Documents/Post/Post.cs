using LoanHub.Backend.UseCases.Features.Documents.Upload;

namespace LoanHub.Backend.Web.Endpoints.Documents.Post;

public class Post(IMediator mediator) : Endpoint<UploadDocumentRequest>
{
	public override void Configure()
	{
		Version(1);
		AllowAnonymous();
		AllowFileUploads();
		Post("/documents");
	}

	public override async Task HandleAsync(UploadDocumentRequest req, CancellationToken ct)
	{
		var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

		int? requestingUserId = null;

		if (!string.IsNullOrEmpty(userIdClaim) && int.TryParse(userIdClaim, out var id))
		{
			requestingUserId = id;
		}

		var command = new UploadDocumentCommand(
			requestingUserId,
			req.ApplicationId,
			req.DocumentId,
			req.Document.OpenReadStream(),
			req.Document.ContentType);

		var result = await mediator.Send(command, ct);

		await result.SendResult(this, ct);
	}
}