using LoanHub.Backend.UseCases.Features.Offer.Get;

namespace LoanHub.Backend.Web.Endpoints.Application.Get;

/// <summary>
/// Get an Application by integer ID.
/// </summary>
/// <remarks>
/// Takes a positive integer ID and returns a matching Application record.
/// </remarks>
public class Get(IMediator mediator) : Endpoint<GetApplicationByIdRequest, ApplicationDto>
{
	public override void Configure()
	{
		AllowAnonymous();
		Version(1);
		Get("/applications/{ApplicationId:int}");
	}

	public override async Task HandleAsync(GetApplicationByIdRequest req, CancellationToken ct)
	{
		var request = new GetOfferByIdQuery(req.ApplicationId);
		var result = await mediator.Send(request, ct);

		await result.SendResult(this, ct: ct);
	}
}