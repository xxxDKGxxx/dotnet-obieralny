namespace LoanHub.Aggregator.Web.Endpoints.Applications.Update;

public class UpdateStatus(IEnumerable<IOfferProvider> offerProviders) :
	Endpoint<UpdateApplicationStatusRequest, ApplicationWithProviderTypeDto>
{
	public override void Configure()
	{
		AllowAnonymous();
		Put("/applications/{ApplicationId:int}/status");
	}

	public override async Task HandleAsync(UpdateApplicationStatusRequest req, CancellationToken ct)
	{
		var offerProvider =
			offerProviders.Single(o =>
			{
				return o.ProviderType == ApplicationProviderType.FromValue(req.ProviderType);
			});

		var newStatusEnum = ApplicationStatus.FromValue(req.NewStatus);
		var newApplication = await offerProvider.UpdateStatusAsync(
			req.ApplicationId,
			newStatusEnum,
			req.StatusChangeMessage);

		Response = newApplication;
	}
}