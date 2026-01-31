namespace LoanHub.Aggregator.Web.Endpoints.Applications.Update;

public class UpdateStatus(IEnumerable<IOfferProvider> offerProviders) :
	Endpoint<UpdateApplicationStatusRequest, ApplicationWithProviderTypeDto>
{
	public override void Configure()
	{
		Put("/applications/{ApplicationId:int}/status");
		Policies("DefaultPolicy");
	}

	public override async Task HandleAsync(UpdateApplicationStatusRequest req, CancellationToken ct)
	{
		var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

		if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var requestingUserId))
		{
			await SendUnauthorizedAsync(ct);
			return;
		}

		var offerProvider =
			offerProviders.Single(o =>
			{
				return o.ProviderType == ApplicationProviderType.FromValue(req.ProviderType);
			});

		var newStatusEnum = ApplicationStatus.FromValue(req.NewStatus);
		var newApplication = await offerProvider.UpdateStatusAsync(
			req.ApplicationId,
			newStatusEnum,
			req.StatusChangeMessage,
			requestingUserId,
			ct);

		Response = newApplication;
	}
}