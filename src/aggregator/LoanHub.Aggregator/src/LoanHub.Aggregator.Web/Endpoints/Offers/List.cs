using LoanHub.Aggregator.Core.Interfaces;
using LoanHub.Aggregator.Web.Dtos;

namespace LoanHub.Aggregator.Web.Endpoints.Offers;

public class List(IEnumerable<IOfferProvider> offerProviders) :
	Endpoint<ListOffersRequest, IEnumerable<OfferWithProviderTypeDto>>
{
	public override void Configure()
	{
		Get("/offers");
		AllowAnonymous();
	}

	public override async Task HandleAsync(ListOffersRequest req, CancellationToken ct)
	{
		var tasks = offerProviders.Select(async provider =>
		{
			try
			{
				var offers = await provider.ListOffersAsync(req.Amount, req.Duration);
				return (Offers: offers, ProviderType: provider.ProviderType.Value);
			}
			catch (Exception ex)
			{
				Logger.LogError("{Message}", ex.Message);

				return (Offers: [], ProviderType: provider.ProviderType.Value);
			}
		});

		var offerCollections = await Task.WhenAll(tasks);

		var result = new List<OfferWithProviderTypeDto>();

		foreach (var (offerCollection, providerType) in offerCollections)
		{
			result.AddRange(
				offerCollection.Select(
					od =>
					{
						return new OfferWithProviderTypeDto(
												od.Id,
												od.Title,
												od.Description,
												od.MinAmount,
												od.MaxAmount,
												od.MinDuration,
												od.MaxDuration,
												od.MinInterestRate,
												od.MaxInterestRate,
												od.ValidFrom,
												od.ValidTo,
												providerType);
					}));
		}

		Response = result;
	}
}