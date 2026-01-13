namespace LoanHub.Backend.UseCases.Features.Offer.Get;

public sealed class GetOfferByIdQueryHandler(IReadRepository<OfferEntity> offersRepository, IMapper mapper) :
	IQueryHandler<GetOfferByIdQuery, Result<OfferDto>>
{
	public async Task<Result<OfferDto>> Handle(GetOfferByIdQuery request, CancellationToken cancellationToken)
	{
		var spec = new OfferByIdSpec(request.OfferId);
		var offer = await offersRepository.SingleOrDefaultAsync(spec, cancellationToken);

		return offer is null ? Result.NotFound() : Result.Success(mapper.Map<OfferDto>(offer));
	}
}