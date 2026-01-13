namespace LoanHub.Backend.Web.Endpoints.User;

public sealed class UpdateUser(IMediator mediator) : Endpoint<UpdateUserRequest, UserProfileDto>
{
	public override void Configure()
	{
		Put("/users/{userId:int}");
		Version(1);
		Summary(s =>
		{
			s.Summary = "Update user profile";
			s.Description = "Allows a user to update their own profile. Only the user themselves can update their profile. Returns 403 if userId in token does not match userId in route.";
		});
	}

	public override async Task HandleAsync(UpdateUserRequest req, CancellationToken ct)
	{
		var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
		if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userIdFromToken))
		{
			await SendUnauthorizedAsync(ct);
			return;
		}

		if (Route<int>("userId") != userIdFromToken)
		{
			await SendForbiddenAsync(ct);
			return;
		}

		var command = new UpdateUserCommand(userIdFromToken, req);
		var result = await mediator.Send(command, ct);
		await result.SendResult(this, ct);
	}
}