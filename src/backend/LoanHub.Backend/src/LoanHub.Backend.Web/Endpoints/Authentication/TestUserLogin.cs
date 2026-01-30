namespace LoanHub.Backend.Web.Endpoints.Authentication.TestUserLogin;

public sealed class TestUserLogin(
	IHostEnvironment environment,
	IMediator mediator) : Endpoint<TestUserLoginRequest, TokenDto>
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

	public override async Task HandleAsync(TestUserLoginRequest request, CancellationToken ct)
	{
		if (environment.IsDevelopment())
		{
			var command = new TestLoginCommand(request.Email);
			var result = await mediator.Send(command, ct);

			await result.SendResult(this, ct);
		}
		else
		{
			HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
		}
	}
}