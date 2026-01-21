namespace LoanHub.Aggregator.Core.Interfaces.Dtos;

public sealed record CalculatedOfferWithProviderTypeDto(
	int Id,
	string Title,
	string Description,
	decimal Amount,
	uint Duration,
	decimal InterestRate,
	DateTime ValidFrom,
	DateTime ValidTo,
	string ProviderType
);