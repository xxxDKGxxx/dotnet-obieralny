namespace LoanHub.Backend.Web.Endpoints.Applications;

public class GetApplicationByIdValidator : Validator<GetApplicationByIdRequest>
{
	public GetApplicationByIdValidator()
	{
		RuleFor(x => x.ApplicationId)
			.GreaterThanOrEqualTo(0);
	}
}