using LoanHub.Aggregator.Core.Interfaces;
using LoanHub.Aggregator.Web.Dtos;

namespace LoanHub.Aggregator.Web.Endpoints.Offers;

public class GetCalculated(IEnumerable<IOfferProvider> offerProviders) : Endpoint<GetCalculatedOfferRequest, CalculatedOfferWithProviderTypeDto>
{
	public override void Configure()
	{
		AllowAnonymous();
		Get("/calculated-offers/{OfferId:int}");
	}

	public override async Task HandleAsync(GetCalculatedOfferRequest req, CancellationToken ct)
	{
		var provider = offerProviders.Single(op =>
		{
			return op.ProviderType == ApplicationProviderType.FromValue(req.ProviderType);
		});
		var calculatedOffer = await provider.GetCalculatedOfferByIdAsync(
			req.OfferId,
			req.Amount,
			req.Duration,
			req.MonthlyIncome,
			req.MonthlyCosts,
			req.Age,
			req.Dependants);

		Response = new CalculatedOfferWithProviderTypeDto(
			calculatedOffer.Id,
			calculatedOffer.Title,
			calculatedOffer.Description,
			calculatedOffer.Amount,
			calculatedOffer.Duration,
			calculatedOffer.InterestRate,
			calculatedOffer.ValidFrom,
			calculatedOffer.ValidTo,
			provider.ProviderType.Value);
	}
}