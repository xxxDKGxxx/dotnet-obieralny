namespace LoanHub.Backend.UseCases.Features.Documents.Download;

public record DownloadDocumentQuery(int RequestingUserId, int ApplicationId, string DocumentId) :
	IQuery<Result<ApplicationDocumentDto>>;