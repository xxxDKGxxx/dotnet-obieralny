using LoanHub.Aggregator.Core.ApplicationAggregate;

namespace LoanHub.Aggregator.Web.Dtos;

public sealed record CalculatedOfferWithProviderTypeDto(
	int Id,
	string Title,
	string Description,
	decimal Amount,
	uint Duration,
	decimal InterestRate,
	DateTime ValidFrom,
	DateTime ValidTo,
	ApplicationProviderType ProviderType
);