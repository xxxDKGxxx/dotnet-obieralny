namespace LoanHub.Aggregator.Web.Endpoints.Applications.List;

public record ListApplicationsRequest(int? UserId, string? ProviderType);