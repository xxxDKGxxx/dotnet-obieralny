namespace LoanHub.Aggregator.Web.Endpoints.Documents.Get;

public class GetTemplateValidator : Validator<GetTemplateRequest>
{
	public GetTemplateValidator()
	{
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