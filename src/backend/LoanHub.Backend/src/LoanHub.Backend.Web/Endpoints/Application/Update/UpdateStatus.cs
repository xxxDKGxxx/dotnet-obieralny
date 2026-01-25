using LoanHub.Backend.UseCases.Features.Application.Update;

namespace LoanHub.Backend.Web.Endpoints.Application.Update;

public class UpdateStatus(IMediator mediator) : Endpoint<UpdateApplicationStatusRequest, ApplicationDto>
{
	public override void Configure()
	{
		Version(1);
		Policies("DefaultPolicy");
		Put("/applications/{ApplicationId:int}/status");
	}

	public override async Task HandleAsync(UpdateApplicationStatusRequest req, CancellationToken ct)
	{
		var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

		if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
		{
			await SendUnauthorizedAsync(ct);
			return;
		}

		var command = new UpdateApplicationStatusCommand(
			req.ApplicationId,
			ApplicationStatus.FromValue(req.NewStatus),
			userId,
			req.StatusChangeMessage);

		var result = await mediator.Send(command, ct);
		await result.SendResult(this, ct: ct);
	}
}