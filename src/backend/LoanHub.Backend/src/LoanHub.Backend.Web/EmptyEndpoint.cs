namespace LoanHub.Backend.Web;

public sealed class EmptyEndpoint : EndpointWithoutRequest<string>
{
	public override void Configure()
	{
		Get("/api/mock");
		AllowAnonymous();
	}

	public override async Task HandleAsync(CancellationToken ct)
	{
		await SendAsync("This is a mock response", cancellation: ct);
	}
}
