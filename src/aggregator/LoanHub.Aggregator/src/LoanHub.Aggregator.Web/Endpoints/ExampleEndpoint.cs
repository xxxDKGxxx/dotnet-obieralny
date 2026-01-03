namespace LoanHub.Aggregator.Web.Endpoints;

public sealed class ExampleEndpoint : EndpointWithoutRequest<string>
{
	public override void Configure()
	{
		Get("/hello-world");
		AllowAnonymous();
	}

	public override Task HandleAsync(CancellationToken ct)
	{
		Response = "Hello, World!";
		return Task.CompletedTask;
	}
}