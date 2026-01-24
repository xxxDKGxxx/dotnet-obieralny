namespace LoanHub.Backend.Web.Endpoints.Application.Update;

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
	}
}