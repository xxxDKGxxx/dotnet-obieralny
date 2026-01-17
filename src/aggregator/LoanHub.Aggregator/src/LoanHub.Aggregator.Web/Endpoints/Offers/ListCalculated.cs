using LoanHub.Aggregator.Core.Interfaces;
using LoanHub.Aggregator.Web.Dtos;

namespace LoanHub.Aggregator.Web.Endpoints.Offers;

public sealed class ListCalculated(IEnumerable<IOfferProvider> offerProviders) :
	Endpoint<ListCalculatedOffersRequest, IEnumerable<CalculatedOfferWithProviderTypeDto>>
{
	public override void Configure()
	{
		Get("/calculated-offers");
		AllowAnonymous();
	}

	public override async Task HandleAsync(ListCalculatedOffersRequest req, CancellationToken ct)
	{
		var tasks = offerProviders.Select(async provider =>
		{
			try
			{
				var offers = await provider.ListCalculatedOffersAsync(
					req.Amount,
					req.Duration,
					req.MonthlyIncome,
					req.MonthlyCosts,
					req.Age,
					req.Dependants);

				return offers;
			}
			catch (Exception ex)
			{
				Logger.LogError("{Message}", ex.Message);

				return [];
			}
		});

		var offerCollections = await Task.WhenAll(tasks);

		var result = new List<CalculatedOfferWithProviderTypeDto>();

		foreach (var offerCollection in offerCollections)
		{
			result.AddRange(offerCollection);
		}

		Response = result;
	}
}