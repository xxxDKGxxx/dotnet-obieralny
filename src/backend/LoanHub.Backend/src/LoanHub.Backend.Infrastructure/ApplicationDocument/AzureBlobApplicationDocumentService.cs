namespace LoanHub.Backend.Infrastructure.ApplicationDocument;

public sealed class AzureBlobApplicationDocumentService : IApplicationDocumentService
{
	private readonly BlobContainerClient _containerClient;

	public AzureBlobApplicationDocumentService(IConfiguration configuration)
	{
		var connectionString = configuration.GetValue<string>("AzureBlob:ConnectionString");
		var containerName = configuration.GetValue<string>("AzureBlob:Container");

		Guard.Against.NullOrEmpty(connectionString);
		Guard.Against.NullOrEmpty(containerName);

		_containerClient = new BlobContainerClient(connectionString, containerName);
	}

	public async Task<string> UploadNewAsync(
		Stream content,
		int applicationId,
		string contentType,
		string fileExtension,
		CancellationToken cancellationToken = default)
	{
		if (!fileExtension.StartsWith('.'))
		{
			fileExtension = "." + fileExtension;
		}

		var fileName = $"{applicationId}_{Guid.NewGuid()}{fileExtension}";

		return await UploadAsync(content, fileName, contentType, cancellationToken);
	}

	public async Task<string> UploadAsync(
		Stream content,
		string documentId,
		string contentType,
		CancellationToken cancellationToken = default)
	{
		Guard.Against.Null(content);
		Guard.Against.NullOrEmpty(contentType);

		var blobClient = _containerClient.GetBlobClient(documentId);

		var uploadOptions = new BlobUploadOptions
		{
			HttpHeaders = new BlobHttpHeaders { ContentType = contentType }
		};

		await blobClient.UploadAsync(content, uploadOptions, cancellationToken);

		return documentId;
	}

	public async Task<Stream> DownloadAsync(string documentId, CancellationToken cancellationToken = default)
	{
		Guard.Against.NullOrEmpty(documentId);

		var blobClient = _containerClient.GetBlobClient(documentId);
		var response = await blobClient.DownloadStreamingAsync(cancellationToken: cancellationToken);

		return response.Value.Content;
	}
}