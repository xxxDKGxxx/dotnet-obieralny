using LoanHub.Backend.UseCases.Features.Authentication.Login;
using LoanHub.Backend.Web.Endpoints.Shared;

namespace LoanHub.Backend.Web.Endpoints.Authentication;

public sealed class GoogleLoginEndpoint(
	IMediator mediator,
	ILogger<GoogleLoginEndpoint> logger) : Endpoint<GoogleAuthRequest, GoogleAuthResponse>
{
	public override void Configure()
	{
		Post("/api/auth/google-login");
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
			var response = new GoogleAuthResponse
			{
				AccessToken = result.Value.AccessToken
			};

			await SendOkAsync(response, ct);
		}
		else
		{
			logger.LogWarning("Google login failed: {Error}", result.Status);
			await SendUnauthorizedAsync(ct);
		}
	}
}