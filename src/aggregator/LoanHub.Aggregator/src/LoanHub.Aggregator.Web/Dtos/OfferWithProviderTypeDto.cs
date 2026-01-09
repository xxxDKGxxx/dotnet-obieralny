using LoanHub.Aggregator.Core.ApplicationAggregate;

namespace LoanHub.Aggregator.Web.Dtos;

public sealed record OfferWithProviderTypeDto(
	int Id,
	string Title,
	string Description,
	decimal MinAmount,
	decimal MaxAmount,
	uint MinDuration,
	uint MaxDuration,
	decimal MinInterestRate,
	decimal MaxInterestRate,
	DateTime ValidFrom,
	DateTime ValidTo,
	string ProviderType
);