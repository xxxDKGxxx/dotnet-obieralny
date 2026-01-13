namespace LoanHub.Aggregator.Web.Endpoints.Applications;

public record GetApplicationByIdRequest(int ApplicationId, string ProviderType);