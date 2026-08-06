namespace LoanHub.Backend.UseCases.Features.Offer.List;

public sealed record ListOffersQuery(decimal Amount, uint Duration) : IQuery<Result<IEnumerable<OfferDto>>>;