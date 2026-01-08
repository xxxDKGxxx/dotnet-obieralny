using LoanHub.Backend.UseCases.Features.Applications.Get;

namespace LoanHub.Backend.Web.Endpoints.Applications;

/// <summary>
/// Get an Application by integer ID.
/// </summary>
/// <remarks>
/// Takes a positive integer ID and returns a matching Application record.
/// </remarks>
public class GetById(IMediator _mediator)
  : Endpoint<GetApplicationByIdRequest, ApplicationRecord>
{
	public override void Configure()
	{
		Get(GetApplicationByIdRequest.Route);
		AllowAnonymous();
	}

	public override async Task HandleAsync(GetApplicationByIdRequest request,
	  CancellationToken cancellationToken)
	{
		var query = new GetApplicationQuery(request.ApplicationId);

		var result = await _mediator.Send(query, cancellationToken);

		if (result.Status == ResultStatus.NotFound)
		{
			await SendNotFoundAsync(cancellationToken);
			return;
		}

		if (result.IsSuccess)
		{
			Response = new ApplicationRecord(result.Value.Id, result.Value.Title, result.Value.);
		}
	}
}
