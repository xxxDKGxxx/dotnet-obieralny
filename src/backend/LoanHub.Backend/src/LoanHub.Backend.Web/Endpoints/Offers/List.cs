using LoanHub.Backend.Web.Extensions;

namespace LoanHub.Backend.Web.Endpoints.Offers;

public class List(IMediator mediator) : Endpoint<ListOffersRequest, IEnumerable<OfferDto>>
{
	public override void Configure()
	{
		Get("/offers");
		Version(1);
		AllowAnonymous();
	}

	public override async Task HandleAsync(ListOffersRequest req, CancellationToken ct)
	{
		var request = new ListOffersQuery(req.Amount, req.Duration);
		var result = await mediator.Send(request, ct);

		await result.SendResult(this, ct);
	}
}