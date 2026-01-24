namespace LoanHub.Aggregator.Web.Endpoints.Applications.List;

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

		RuleFor(request => request.ProviderType)
			.NotEmpty()
			.When(request =>
			{
				return request.ProviderType is not null;
			});
	}
}