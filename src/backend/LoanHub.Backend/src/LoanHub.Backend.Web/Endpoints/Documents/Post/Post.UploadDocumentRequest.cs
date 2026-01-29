namespace LoanHub.Backend.Web.Endpoints.Documents.Post;

public record UploadDocumentRequest(
	int ApplicationId,
	string DocumentId,
	IFormFile Document);