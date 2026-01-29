namespace LoanHub.Aggregator.Web.Endpoints.Documents.Post;

public record UploadDocumentRequest(
	int ApplicationId,
	string DocumentId,
	IFormFile Document,
	string ProviderType);