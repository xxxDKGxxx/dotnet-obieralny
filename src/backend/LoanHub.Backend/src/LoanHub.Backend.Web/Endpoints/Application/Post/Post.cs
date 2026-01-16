using LoanHub.Backend.UseCases.Features.Application.Create;

namespace LoanHub.Backend.Web.Endpoints.Application.Post;

public sealed class Post(IMediator mediator) : Endpoint<PostApplicationRequest, ApplicationDto>
{
	public override void Configure()
	{
		AllowAnonymous();
		Version(1);
		Post("/applications");
	}

	public override async Task HandleAsync(PostApplicationRequest req, CancellationToken ct)
	{
		var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

		int? userId = null;

		if (userIdClaim is not null && int.TryParse(userIdClaim, out var userIdParsed))
		{
			userId = userIdParsed;
		}

		var command = new CreateApplicationCommand(
			req.OfferId,
			req.UserId,
			userId,
			req.Amount,
			req.Duration,
			req.Financials,
			req.Contact,
			req.PersonalData);

		var result = await mediator.Send(command, ct);

		await result.SendResult(this, ct: ct);
	}
}