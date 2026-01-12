using LoanHub.Backend.UseCases.Features.Offer.Get;

namespace LoanHub.Backend.Web.Endpoints.Offers.Get;

public class GetCalculated(IMediator mediator) : Endpoint<GetCalculatedOfferRequest, Result<CalculatedOfferDto>>
{
	public override void Configure()
	{
		AllowAnonymous();
		Version(1);
		Get("/calculated-offers/{OfferId:int}");
	}

	public override async Task HandleAsync(GetCalculatedOfferRequest req, CancellationToken ct)
	{
		var query = new GetCalculatedOfferByIdQuery(
			req.OfferId,
			req.Amount,
			req.Duration,
			req.MonthlyIncome,
			req.MonthlyCosts,
			req.Age,
			req.Dependants);
		var result = await mediator.Send(query, ct);

		await result.SendResult(this, ct: ct);
	}
}