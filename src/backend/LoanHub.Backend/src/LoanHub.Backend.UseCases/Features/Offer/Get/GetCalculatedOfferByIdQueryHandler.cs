namespace LoanHub.Backend.UseCases.Features.Offer.Get;

public sealed class GetCalculatedOfferByIdQueryHandler(
	IReadRepository<OfferEntity>  offerRepository,
	IOfferCalculator offerCalculator,
	IMapper mapper)  :
	IQueryHandler<GetCalculatedOfferByIdQuery, Result<CalculatedOfferDto>>
{
	public async Task<Result<CalculatedOfferDto>> Handle(
		GetCalculatedOfferByIdQuery request,
		CancellationToken cancellationToken)
	{
		var spec = new OfferByIdSpec(request.OfferId);
		var offer = await offerRepository.SingleOrDefaultAsync(spec, cancellationToken);

		if (offer is null)
		{
			return Result.NotFound();
		}

		var offerConditions = offerCalculator.Calculate(
			offer,
			request.Amount,
			request.Duration,
			request.MonthlyIncome,
			request.MonthlyCosts,
			request.Age,
			request.Dependants);

		return Result.Success(mapper.Map<CalculatedOfferDto>((offer, offerConditions)));
	}
}