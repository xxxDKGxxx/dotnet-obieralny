using LoanHub.Backend.Web.Endpoints.Documents;

namespace LoanHub.Backend.Web.Email;

public class EmailLinkProvider : IEmailLinkProvider
{
	private readonly string _frontendOrigin;
	private readonly string _templateLink;

	public EmailLinkProvider(
		LinkGenerator linkGenerator,
		IConfiguration configuration)
	{
		_frontendOrigin = configuration["FrontendOrigin"]
							   ?? throw new Exception("Frontend origin missing from configuration");

		var baseUrl = configuration["BaseUrl"] ?? throw new Exception("Base url missing from configuration");
		var endpointPath = linkGenerator.GetPathByName(typeof(GetTemplate).FullName!);

		_templateLink = $"{baseUrl.TrimEnd('/')}{endpointPath}";
	}

	public string GetTemplateLink()
	{
		return _templateLink;
	}

	public string GetUploadLink(string documentId, int applicationId)
	{
		return $"{_frontendOrigin}/upload-document"
			+ $"?applicationId={applicationId}"
			+ $"&documentId={documentId}"
			+ $"&providerType=ArdalisBank";
	}
}