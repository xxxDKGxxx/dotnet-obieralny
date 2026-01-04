namespace LoanHub.Backend.UseCases.Mapping.Profiles;

public class OfferProfile : Profile
{
	public OfferProfile()
	{
		CreateMap<OfferEntity, OfferDto>()
			.ForCtorParam(nameof(OfferDto.MinAmount), opt =>
			{
				opt.MapFrom(o => o.AmountRange.Min);
			})
			.ForCtorParam(nameof(OfferDto.MaxAmount), opt =>
			{
				opt.MapFrom(o => o.AmountRange.Max);
			})
			.ForCtorParam(nameof(OfferDto.MinDuration), opt =>
			{
				opt.MapFrom(o => o.DurationRange.Min);
			})
			.ForCtorParam(nameof(OfferDto.MaxDuration), opt =>
			{
				opt.MapFrom(o => o.DurationRange.Max);
			})
			.ForCtorParam(nameof(OfferDto.MinInterestRate), opt =>
			{
				opt.MapFrom(o => o.InterestRateRange.Min);
			})
			.ForCtorParam(nameof(OfferDto.MaxInterestRate), opt =>
			{
				opt.MapFrom(o => o.InterestRateRange.Max);
			})
			.ForCtorParam(nameof(OfferDto.ValidFrom), opt =>
			{
				opt.MapFrom(o => o.ValidRange.Min);
			})
			.ForCtorParam(nameof(OfferDto.ValidTo), opt =>
			{
				opt.MapFrom(o => o.ValidRange.Max);
			});

		CreateMap<(OfferEntity, OfferConditionsDto), CalculatedOfferDto>()
			.ForCtorParam(nameof(CalculatedOfferDto.Amount), opt =>
			{
				opt.MapFrom(s => s.Item2.Amount);
			})
			.ForCtorParam(nameof(CalculatedOfferDto.Duration), opt =>
			{
				opt.MapFrom(s => s.Item2.Duration);
			})
			.ForCtorParam(nameof(CalculatedOfferDto.InterestRate), opt =>
			{
				opt.MapFrom(s => s.Item2.InterestRate);
			})
			.ForCtorParam(nameof(CalculatedOfferDto.Id), opt =>
			{
				opt.MapFrom(o => o.Item1.Id);
			})
			.ForCtorParam(nameof(CalculatedOfferDto.Description), opt =>
			{
				opt.MapFrom(o => o.Item1.Description);
			})
			.ForCtorParam(nameof(CalculatedOfferDto.Title), opt =>
			{
				opt.MapFrom(o => o.Item1.Title);
			})
			.ForCtorParam(nameof(CalculatedOfferDto.ValidFrom), opt =>
			{
				opt.MapFrom(o => o.Item1.ValidRange.Min);
			})
			.ForCtorParam(nameof(CalculatedOfferDto.ValidTo), opt =>
			{
				opt.MapFrom(o => o.Item1.ValidRange.Max);
			});
	}
}