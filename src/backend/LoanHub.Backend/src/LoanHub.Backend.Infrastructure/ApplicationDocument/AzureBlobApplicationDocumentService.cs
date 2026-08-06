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

	public string ReserveNew(int applicationId)
	{
		return $"{applicationId}_{Guid.NewGuid()}.docx";
	}

	public async Task<string> UploadAsync(
		Stream content,
		string documentId,
		string contentType,
		CancellationToken cancellationToken = default)
	{
		Guard.Against.Null(content);
		Guard.Against.NullOrEmpty(contentType);
		Guard.Against.NullOrEmpty(documentId);

		var extension = Path.GetExtension(documentId);

		if (extension is null || !extension.Equals(".docx", StringComparison.InvariantCultureIgnoreCase))
		{
			throw new ArgumentException($"The document id '{documentId}' is not a valid document format. "
										+ $"Expected docx");
		}

		var blobClient = _containerClient.GetBlobClient(documentId);

		var uploadOptions = new BlobUploadOptions
		{
			HttpHeaders = new BlobHttpHeaders { ContentType = contentType }
		};

		await blobClient.UploadAsync(
			content,
			uploadOptions,
			cancellationToken);

		return documentId;
	}

	public async Task<ApplicationDocumentDto> DownloadAsync(
		string documentId,
		CancellationToken cancellationToken = default)
	{
		Guard.Against.NullOrEmpty(documentId);

		var blobClient = _containerClient.GetBlobClient(documentId);
		var response = await blobClient.DownloadStreamingAsync(cancellationToken: cancellationToken);

		return new ApplicationDocumentDto(
			response.Value.Content,
			response.Value.Details.ContentType,
			documentId);
	}
}