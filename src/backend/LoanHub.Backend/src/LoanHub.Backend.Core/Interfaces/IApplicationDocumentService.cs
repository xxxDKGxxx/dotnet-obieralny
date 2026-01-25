namespace LoanHub.Backend.Core.Interfaces;

public interface IApplicationDocumentService
{
	public Task<string> UploadNewAsync(
		Stream content,
		int applicationId,
		string contentType,
		string fileExtension,
		CancellationToken cancellationToken = default);
	public Task<string> UploadAsync(
		Stream content,
		string documentId,
		string contentType,
		CancellationToken cancellationToken = default);
	public Task<Stream> DownloadAsync(string documentId, CancellationToken cancellationToken = default);
}