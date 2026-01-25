namespace LoanHub.Aggregator.Web.Endpoints.Applications.Get;

public sealed class GetApplicationByIdValidator : Validator<GetApplicationByIdRequest>
{
	public GetApplicationByIdValidator()
	{
		RuleFor(request => request.ApplicationId)
			.GreaterThanOrEqualTo(0);
		RuleFor(request => request.ProviderType)
			.Custom((providerType, context) =>
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