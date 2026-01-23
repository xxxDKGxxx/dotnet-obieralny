using FluentValidation;

namespace LoanHub.Aggregator.Web.Endpoints.Applications;

public sealed class GetApplicationByIdValidator : Validator<GetApplicationByIdRequest>
{
	public GetApplicationByIdValidator()
	{
		RuleFor(request => request.ApplicationId).
			GreaterThanOrEqualTo(0);
		RuleFor(request => request.ProviderType).
			NotNull();
	}
}