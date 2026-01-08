namespace LoanHub.Backend.Web.Endpoints.Applications;
public class GetApplicationByIdRequest
{
	public const string Route = "/Applications/{applicationId:int}";
	public static string BuildRoute(int applicationId) => Route.Replace("{ContributorId:int}", applicationId.ToString());

	public int ApplicationId { get; set; }
}