using LoanHub.Backend.Web.Endpoints.Applications;

namespace LoanHub.Backend.Web.Endpoints.Application.Get;

public class GetApplicationByIdValidator : Validator<GetApplicationByIdRequest>
{
	public GetApplicationByIdValidator()
	{
		RuleFor(x => x.ApplicationId)
			.GreaterThanOrEqualTo(0);
	}
}