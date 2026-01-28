namespace LoanHub.Aggregator.Web.Endpoints.Counter.Get;

public class Get(ICounterRepository counterRepository) : EndpointWithoutRequest<CounterDto>
{
	public override void Configure()
	{
		AllowAnonymous();
		Get("/applications-counter");
	}

	public override async Task HandleAsync(CancellationToken ct)
	{
		var counter = await counterRepository.GetByIdAsync(DataSchemaConstants.CounterId, ct);

		if (counter is null)
		{
			await SendNotFoundAsync(ct);
			return;
		}

		Response = new CounterDto(counter.Value);
	}
}