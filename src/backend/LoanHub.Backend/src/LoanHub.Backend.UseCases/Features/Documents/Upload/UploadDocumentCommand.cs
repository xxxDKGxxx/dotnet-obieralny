namespace LoanHub.Backend.UseCases.Features.Documents.Upload;

public record UploadDocumentCommand(
	int? RequestingUserId,
	int ApplicationId,
	string DocumentId,
	Stream Document,
	string ContentType) : ICommand<Result>;