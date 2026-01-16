using LoanHub.Backend.Core.EntityAggregates.UserAggregate;
using LoanHub.Backend.UseCases.Features.Application.Get;
using LoanHub.Backend.UseCases.Features.User.Get;

namespace LoanHub.Backend.Web.Endpoints.Application.Get;

/// <summary>
/// Get an Application by integer ID.
/// </summary>
/// <remarks>
/// Takes a positive integer ID and returns a matching Application record.
/// </remarks>
public class Get(IMediator mediator) : Endpoint<GetApplicationByIdRequest, ApplicationDto>
{
	public override void Configure()
	{
		Version(1);
		Get("/applications/{ApplicationId:int}");
		Summary(s =>
		{
			s.Summary = "Get Application by Id";
			s.Description = "Returns Application data, available to applicants (for their applications) and bank employees (for all)";
		});
	}

	public override async Task HandleAsync(GetApplicationByIdRequest req, CancellationToken ct)
	{
		var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

		if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
		{
			await SendUnauthorizedAsync(ct);
			return;
		}

		var request_application = new GetApplicationByIdQuery(req.ApplicationId);
		var application = await mediator.Send(request_application, ct);

		var request_user = new GetCurrentUserQuery(userId);
		var user = await mediator.Send(request_user, ct);

		if (application.Value.UserId is null || application.Value.UserId != user.Value.Id)
		{
			if (user.Value.Role != UserRole.Employee.Value && user.Value.Role != UserRole.Admin.Value)
			{
				await SendForbiddenAsync(ct);
				return;
			}
		}

		await application.SendResult(this, ct: ct);
	}
}