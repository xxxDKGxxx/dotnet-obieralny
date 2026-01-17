using LoanHub.Backend.UseCases.Features.Audit.List;

namespace LoanHub.Backend.Web.Endpoints.Audits.List;

public class List(IMediator mediator) : Endpoint<ListAuditsRequest, IEnumerable<AuditDto>>
{
	public override void Configure()
	{
		Version(1);
		AllowAnonymous();
		Get("/audits");
	}

	public override async Task HandleAsync(ListAuditsRequest req, CancellationToken ct)
	{
		var query = new ListAuditsFromSpecificDayQuery(req.Day);
		var result = await mediator.Send(query, ct);
		await result.SendResult(this, ct: ct);
	}
}