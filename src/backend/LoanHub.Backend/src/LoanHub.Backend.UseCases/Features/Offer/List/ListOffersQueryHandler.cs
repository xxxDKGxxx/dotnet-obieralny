namespace LoanHub.Backend.UseCases.Features.Offer.List;

public class ListOffersQueryHandler(IReadRepository<OfferEntity> offerRepository, IMapper mapper)
	: IQueryHandler<ListOffersQuery, Result<IEnumerable<OfferDto>>>
{
	public async Task<Result<IEnumerable<OfferDto>>> Handle(ListOffersQuery request, CancellationToken cancellationToken)
	{
		var specification = new OffersByAmountAndDurationSpec(request.Amount, request.Duration);
		var offers = await offerRepository.ListAsync(specification, cancellationToken);

		return Result.Success(offers.Select(mapper.Map<OfferDto>));
	}
}