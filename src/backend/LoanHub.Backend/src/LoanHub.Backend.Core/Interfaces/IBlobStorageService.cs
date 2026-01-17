using System.IO;
using System.Threading.Tasks;

namespace LoanHub.Backend.Core.Interfaces;

public interface IBlobStorageService
{
	public Task<string> UploadAsync(Stream content, string documentId, string contentType);

	public Task<Stream> DownloadAsync(string documentId);
}