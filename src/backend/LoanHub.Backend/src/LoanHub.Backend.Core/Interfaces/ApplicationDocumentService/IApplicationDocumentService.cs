namespace LoanHub.Backend.Core.Interfaces.ApplicationDocumentService;

public interface IApplicationDocumentService
{
	public string ReserveNew(int applicationId);
	public Task<string> UploadAsync(
		Stream content,
		string documentId,
		string contentType,
		CancellationToken cancellationToken = default);
	public Task<ApplicationDocumentDto> DownloadAsync(string documentId, CancellationToken cancellationToken = default);
}