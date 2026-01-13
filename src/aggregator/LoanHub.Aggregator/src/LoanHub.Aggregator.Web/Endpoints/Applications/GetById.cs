using System.Numerics;
using LoanHub.Aggregator.Core.ApplicationAggregate;
using LoanHub.Aggregator.Core.Interfaces;
using LoanHub.Aggregator.Web.Dtos;
using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.Hosting;

namespace LoanHub.Aggregator.Web.Endpoints.Applications;

public class GetById(IEnumerable<IOfferProvider> offerProviders) :
	Endpoint<GetApplicationByIdRequest, ApplicationWithProviderTypeDTO>
{
	public override void Configure()
	{
		Get("/applications/{ApplicationId:int}");
		AllowAnonymous();
	}

	public override async Task HandleAsync(GetApplicationByIdRequest req, CancellationToken ct)
	{
		var provider = offerProviders.Single(op =>
		{
			return op.ProviderType == ApplicationProviderType.FromValue(req.ProviderType);
		});
		var application = await provider.GetApplicationByIdAsync(req.ApplicationId);

		Response = new ApplicationWithProviderTypeDTO(
			application.Id,
			application.Title,
			application.Description,
			application.Status,
			application.Amount,
			application.Duration,
			application.InterestRate,
			application.UpdatedAt,
			application.Email,
			application.FirstName,
			application.LastName,
			application.Address,
			application.Phone,
			application.Job,
			application.Income,
			application.Costs,
			application.Age,
			application.Dependents,
			provider.ProviderType.Value
			);
	}
}