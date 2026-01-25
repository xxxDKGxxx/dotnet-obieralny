namespace LoanHub.Aggregator.Web.Endpoints.Applications.Update;

public class UpdateApplicationStatusValidator : Validator<UpdateApplicationStatusRequest>
{
	public UpdateApplicationStatusValidator()
	{
		RuleFor(x => x.ApplicationId).
			GreaterThanOrEqualTo(0);

		RuleFor(x => x.NewStatus).
			Custom((status, context) =>
			{
				try
				{
					ApplicationStatus.FromValue(status);
				}
				catch (KeyNotFoundException)
				{
					context.AddFailure(
						nameof(UpdateApplicationStatusRequest.NewStatus),
						"Not a valid status");
				}
			});

		RuleFor(x => x.ProviderType).
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

		RuleFor(x => x.StatusChangeMessage)
			.NotEmpty()
			.When(x =>
			{
				return ApplicationStatus.FromValue(x.NewStatus) == ApplicationStatus.AwaitingAmendments;
			});
	}
}