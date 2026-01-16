<<<<<<< HEAD
namespace LoanHub.Backend.Web.Endpoints.Offers.Get;

public sealed record GetOfferRequest(int OfferId);
=======
namespace LoanHub.Backend.Web.Endpoints.Applications;
public class GetApplicationByIdRequest
{
	public const string Route = "/Applications/{applicationId:int}";
	public static string BuildRoute(int applicationId) => Route.Replace("{ContributorId:int}", applicationId.ToString());

	public int ApplicationId { get; set; }
}
>>>>>>> 7ad7476eeea0e2f4c266a6307f360e22ed70bb07
