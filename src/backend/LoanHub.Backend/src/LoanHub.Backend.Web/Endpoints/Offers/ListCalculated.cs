using LoanHub.Backend.Core.EntityAggregates.OfferAggregate.Dto;
using LoanHub.Backend.Web.Extensions;

namespace LoanHub.Backend.Web.Endpoints.Offers;

public class ListCalculated(IMediator mediator) : Endpoint<ListCalculatedOffersRequest, IEnumerable<CalculatedOfferDto>>
{
	public override void Configure()
	{
		Get("/calculated-offers");
		Version(1);
		AllowAnonymous();
	}

	public override async Task HandleAsync(ListCalculatedOffersRequest req, CancellationToken ct)
	{
		var request = new ListCalculatedOffersQuery(
			req.Amount,
			req.Duration,
			req.MonthlyIncome,
			req.MonthlyCosts,
			req.Age,
			req.Dependants);

		var result = await mediator.Send(request, ct);

		await result.SendResult(this, ct);
	}
}