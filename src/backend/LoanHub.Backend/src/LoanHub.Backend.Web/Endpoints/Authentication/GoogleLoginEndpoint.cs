using LoanHub.Backend.Web.Extensions;

namespace LoanHub.Backend.Web.Endpoints.Authentication;

public sealed class GoogleLoginEndpoint(
	IMediator mediator) : Endpoint<GoogleAuthRequest, GoogleAuthResponse>
{
	public override void Configure()
	{
		Post("/auth/google-login");
		Version(1);
		AllowAnonymous();
		Summary(s =>
		{
			s.Summary = "Authenticate with Google";
			s.Description = "Validates Google token and returns JWT access token";
		});
	}

	public override async Task HandleAsync(GoogleAuthRequest request, CancellationToken ct)
	{
		var command = new LoginCommand(LoginType.Google, request.Token);
		var result = await mediator.Send(command, ct);

		if (result.IsSuccess)
		{
			await SendOkAsync(new GoogleAuthResponse { AccessToken = result.Value.AccessToken }, ct);
		}
		else
		{
			await result.SendResult(this, ct);
		}
	}
}