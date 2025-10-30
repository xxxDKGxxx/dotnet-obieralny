using LoanHub.Backend.UseCases.Contributors;
using LoanHub.Backend.UseCases.Contributors.List;

namespace LoanHub.Backend.Web.Contributors;

/// <summary>
/// List all Contributors
/// </summary>
/// <remarks>
/// List all contributors - returns a ContributorListResponse containing the Contributors.
/// </remarks>
public class List(IMediator _mediator) : EndpointWithoutRequest<ContributorListResponse>
{
    public override void Configure()
    {
        Get("/Contributors");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new ListContributorsQuery(null, null), cancellationToken);

        var result2 = await new ListContributorsQuery2(null, null)
          .ExecuteAsync(cancellationToken);

        if (result.IsSuccess)
        {
            Response = new ContributorListResponse
            {
                Contributors = result.Value.Select(c => new ContributorRecord(c.Id, c.Name, c.PhoneNumber)).ToList()
            };
        }
    }
}
