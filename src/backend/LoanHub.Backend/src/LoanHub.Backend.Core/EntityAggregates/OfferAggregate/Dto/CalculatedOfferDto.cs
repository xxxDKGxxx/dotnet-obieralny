namespace LoanHub.Backend.Core.EntityAggregates.OfferAggregate.Dto;

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