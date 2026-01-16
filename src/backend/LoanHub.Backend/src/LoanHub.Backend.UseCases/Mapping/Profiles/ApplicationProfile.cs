namespace LoanHub.Backend.UseCases.Mapping.Profiles;

public sealed class ApplicationProfile : Profile
{
	public ApplicationProfile()
	{
		CreateMap<OfferConditionsDto, OfferConditions>();
		CreateMap<ApplicationEntity, ApplicationDto>().
			ForCtorParam(
				nameof(ApplicationDto.Status),
				opt =>
				{
					opt.MapFrom(a => a.Status.Value);
				});
	}
}