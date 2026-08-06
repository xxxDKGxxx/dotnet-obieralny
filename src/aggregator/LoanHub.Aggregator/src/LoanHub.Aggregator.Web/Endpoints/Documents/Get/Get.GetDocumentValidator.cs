namespace LoanHub.Aggregator.Web.Endpoints.Documents.Get;

public class GetDocumentValidator : Validator<GetDocumentRequest>
{
	public GetDocumentValidator()
	{
		RuleFor(x => x.DocumentId).
			NotEmpty();

		RuleFor(x => x.ApplicationId)
			.GreaterThanOrEqualTo(0);

		RuleFor(request => request.ProviderType).
			Custom((providerType, context) =>
			{
				try
				{
					ApplicationProviderType.FromValue(providerType);
				}
				catch (KeyNotFoundException)
				{
					context.AddFailure(
						nameof(UpdateApplicationStatusRequest.NewStatus),
						"Not a valid ProviderType");
				}
			});
	}
}