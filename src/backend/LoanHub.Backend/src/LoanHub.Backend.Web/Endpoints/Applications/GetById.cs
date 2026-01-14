using LoanHub.Backend.Core.EntityAggregates.ApplicationAggregate.DTO;
using LoanHub.Backend.UseCases.Features.Application.Get;
using LoanHub.Backend.UseCases.Features.Offer.Get;
namespace LoanHub.Backend.Web.Endpoints.Applications;

public class GetById(IMediator mediator) : Endpoint<GetApplicationByIdRequest, ApplicationDTO>
{
	public override void Configure()
	{
		AllowAnonymous();
		Version(1);
		Get("/applications/{ApplicationId:int}");
	}

	public override async Task HandleAsync(GetApplicationByIdRequest req, CancellationToken ct)
	{
		var request = new GetApplicationByIdQuery(req.ApplicationId);
		var result = await mediator.Send(request, ct);

		await result.SendResult(this, ct: ct);
	}
}