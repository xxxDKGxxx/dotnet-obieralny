namespace LoanHub.Backend.Web.Endpoints.Audits.List;

public class ListAuditsValidator : Validator<ListAuditsRequest>
{
	public ListAuditsValidator()
	{
		RuleFor(r => r.Day)
			.NotEmpty();
	}
}