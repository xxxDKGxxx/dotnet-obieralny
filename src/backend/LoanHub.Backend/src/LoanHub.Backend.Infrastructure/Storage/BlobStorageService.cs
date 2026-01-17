using Ardalis.GuardClauses;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using LoanHub.Backend.Core.Interfaces;
using Microsoft.Extensions.Configuration;

namespace LoanHub.Backend.Infrastructure.Storage;

public sealed class BlobStorageService : IBlobStorageService
{
	private readonly BlobContainerClient _containerClient;

	public BlobStorageService(IConfiguration configuration)
	{
		var connectionString = configuration.GetValue<string>("AzureBlob:ConnectionString");
		var containerName = configuration.GetValue<string>("AzureBlob:Container");

		Guard.Against.NullOrEmpty(connectionString);
		Guard.Against.NullOrEmpty(containerName);

		_containerClient = new BlobContainerClient(connectionString, containerName);
	}

	public async Task<string> UploadAsync(Stream content, string documentId, string contentType)
	{
		Guard.Against.Null(content);
		Guard.Against.NullOrEmpty(documentId);
		Guard.Against.NullOrEmpty(contentType);

		var blobClient = _containerClient.GetBlobClient(documentId);

		var uploadOptions = new BlobUploadOptions
		{
			HttpHeaders = new BlobHttpHeaders { ContentType = contentType }
		};

		await blobClient.UploadAsync(content, uploadOptions).ConfigureAwait(false);

		return blobClient.Name;
	}

	public async Task<Stream> DownloadAsync(string documentId)
	{
		Guard.Against.NullOrEmpty(documentId);

		var blobClient = _containerClient.GetBlobClient(documentId);
		var response = await blobClient.DownloadStreamingAsync().ConfigureAwait(false);

		return response.Value.Content;
	}
}
