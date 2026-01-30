namespace LoanHub.Aggregator.Web.Endpoints.Documents.Get;

public record GetDocumentRequest(string DocumentId, int ApplicationId, string ProviderType);