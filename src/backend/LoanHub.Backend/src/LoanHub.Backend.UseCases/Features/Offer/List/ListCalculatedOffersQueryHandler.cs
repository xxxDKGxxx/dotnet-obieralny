namespace LoanHub.Backend.UseCases.Features.Offer.List;

public sealed class ListCalculatedOffersQueryHandler(
	IOfferCalculator offerCalculator,
	IReadRepository<OfferEntity> offersRepository,
	IMapper mapper) :
	IQueryHandler<ListCalculatedOffersQuery, Result<IEnumerable<CalculatedOfferDto>>>
{
	public async Task<Result<IEnumerable<CalculatedOfferDto>>> Handle(
		ListCalculatedOffersQuery request,
		CancellationToken cancellationToken)
	{
		var specification = new OffersByAmountAndDurationSpec(request.Amount, request.Duration);
		var offers = await offersRepository.ListAsync(specification, cancellationToken);

		var calculatedOffers = offers.Select(o =>
			{
				return mapper.Map<CalculatedOfferDto>((o, offerCalculator.Calculate(
					o,
					request.MonthlyIncome,
					request.MonthlyCosts,
					request.Age,
					request.Dependants)));
			})
			.Where(co =>
			{
				return co.Amount >= request.Amount && co.Duration >= request.Duration;
			});

		return Result.Success(calculatedOffers);
	}
}