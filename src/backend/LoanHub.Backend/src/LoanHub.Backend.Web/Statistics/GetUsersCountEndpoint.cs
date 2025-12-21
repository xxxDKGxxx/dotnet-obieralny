using FastEndpoints;
using LoanHub.Backend.UseCases.Features.Counter;
using MediatR;

namespace LoanHub.Backend.Web.Statistics;

public sealed class GetUsersCountEndpoint : EndpointWithoutRequest<GetUsersCountResult>
{
	private readonly IMediator _mediator;

	public GetUsersCountEndpoint(IMediator mediator)
	{
		_mediator = mediator;
	}

	public override void Configure()
	{
		Get("/api/counter/users-count");
		AllowAnonymous();
	}

	public override async Task HandleAsync(CancellationToken ct)
	{
		var query = new GetUsersCountQuery();
		var result = await _mediator.Send(query, ct);

		await SendAsync(result, cancellation: ct);
	}
}