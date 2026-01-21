namespace LoanHub.Backend.Web.Endpoints.User.Get;

public sealed class GetMe(IMediator mediator) : EndpointWithoutRequest<UserProfileDto>
{
	public override void Configure()
	{
		Get("/users/me");
		Version(1);
		Summary(s =>
		{
			s.Summary = "Get current user information";
			s.Description = "Returns the authenticated user's profile information from the database";
		});
	}

	public override async Task HandleAsync(CancellationToken ct)
	{
		var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

		if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
		{
			await SendUnauthorizedAsync(ct);
			return;
		}

		var query = new GetCurrentUserQuery(userId);
		var result = await mediator.Send(query, ct);

		await result.SendResult(this, ct);
	}
}