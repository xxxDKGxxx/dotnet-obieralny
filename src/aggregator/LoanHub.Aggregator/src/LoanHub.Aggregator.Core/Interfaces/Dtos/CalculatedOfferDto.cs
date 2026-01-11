namespace LoanHub.Aggregator.Core.Interfaces.Dtos;

public sealed record CalculatedOfferDto(
	int Id,
	string Title,
	string Description,
	decimal Amount,
	uint Duration,
	decimal InterestRate,
	DateTime ValidFrom,
	DateTime ValidTo
);