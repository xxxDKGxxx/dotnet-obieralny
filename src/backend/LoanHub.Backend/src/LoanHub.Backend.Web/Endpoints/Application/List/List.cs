using LoanHub.Backend.UseCases.Features.Application.List;

namespace LoanHub.Backend.Web.Endpoints.Application.List;

public class List(IMediator mediator) : Endpoint<ListApplicationsRequest, IEnumerable<ApplicationDto>>
{
	public override void Configure()
	{
		Version(1);
		Get("/applications");
		Policies("DefaultPolicy");
	}

	public override async Task HandleAsync(ListApplicationsRequest req, CancellationToken ct)
	{
		var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

		if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
		{
			await SendUnauthorizedAsync(ct);
			return;
		}

		var query = new ListApplicationsQuery(req.UserId, userId);
		var result = await mediator.Send(query, ct);

		await result.SendResult(this, ct: ct);
	}
}