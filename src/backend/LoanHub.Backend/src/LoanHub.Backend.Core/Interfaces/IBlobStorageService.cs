using System.IO;
using System.Threading.Tasks;

namespace LoanHub.Backend.Core.Interfaces;

public interface IBlobStorageService
{
    Task<string> UploadAsync(Stream content, string documentId, string contentType);

    Task<Stream> DownloadAsync(string documentId);
}