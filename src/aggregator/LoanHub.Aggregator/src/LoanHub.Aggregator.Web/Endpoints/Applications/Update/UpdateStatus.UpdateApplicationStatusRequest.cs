namespace LoanHub.Aggregator.Web.Endpoints.Applications.Update;

public record UpdateApplicationStatusRequest(int ApplicationId, string NewStatus, string ProviderType);