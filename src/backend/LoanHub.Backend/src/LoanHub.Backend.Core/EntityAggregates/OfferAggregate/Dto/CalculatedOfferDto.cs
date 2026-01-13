namespace LoanHub.Backend.UseCases.Features.Offer;

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