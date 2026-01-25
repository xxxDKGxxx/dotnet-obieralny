namespace LoanHub.Backend.Core.Interfaces;

public interface IBlobStorageService
{
	public Task<string> UploadAsync(Stream content, string documentId, string contentType);

	public Task<Stream> DownloadAsync(string documentId);

	public Task<string> UpdateAsync(Stream content, string documentId, string contentType);

	public Task DeleteAsync(string documentId);
}