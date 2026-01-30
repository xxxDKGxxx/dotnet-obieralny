namespace LoanHub.Backend.Core.Interfaces;

public interface IEmailLinkProvider
{
	public string GetTemplateLink();
	public string GetUploadLink(string documentId, int applicationId);
}