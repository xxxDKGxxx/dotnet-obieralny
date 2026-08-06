namespace LoanHub.Backend.Web.Endpoints.Documents.Get;

public class GetDocumentValidator : Validator<GetDocumentRequest>
{
	public GetDocumentValidator()
	{
		RuleFor(x => x.DocumentId).
			NotEmpty();

		RuleFor(x => x.ApplicationId)
			.GreaterThanOrEqualTo(0);
	}
}