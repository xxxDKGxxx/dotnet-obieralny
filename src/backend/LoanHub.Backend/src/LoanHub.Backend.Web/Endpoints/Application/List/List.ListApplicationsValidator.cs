namespace LoanHub.Backend.Web.Endpoints.Application.List;

public class ListApplicationsValidator : Validator<ListApplicationsRequest>
{
	public ListApplicationsValidator()
	{
		RuleFor(request => request.UserId)
			.GreaterThanOrEqualTo(0)
			.When(request =>
			{
				return request.UserId.HasValue;
			});
	}
}