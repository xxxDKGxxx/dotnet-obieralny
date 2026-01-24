namespace LoanHub.Backend.Web.Endpoints.Application.Update;

public record UpdateApplicationStatusRequest(int ApplicationId, string NewStatus);