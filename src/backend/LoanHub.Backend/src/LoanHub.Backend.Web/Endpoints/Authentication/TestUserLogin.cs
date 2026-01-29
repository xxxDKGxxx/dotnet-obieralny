namespace LoanHub.Backend.Web.Endpoints.Authentication;

public sealed class TestUserLogin(
	IMediator mediator) : Endpoint<GoogleAuthRequest, GoogleAuthResponse>
{
	public override void Configure()
	{
		Post("/auth/test-user");
		Version(1);
		AllowAnonymous();
		Summary(s =>
		{
			s.Summary = "Authenticate test user";
			s.Description = "Development (test) environment only";
		});
	}

	public override async Task HandleAsync(GoogleAuthRequest request, CancellationToken ct)
	{
		if()

		var command = new LoginCommand(LoginType.Google, request.Token);
		var result = await mediator.Send(command, ct);

		await result.SendResult(this, ct);
	}
}