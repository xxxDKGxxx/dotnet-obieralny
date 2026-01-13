namespace LoanHub.Backend.Core.EntityAggregates.OfferAggregate.Dto;

public sealed record OfferDto(
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
	DateTime ValidTo
);