namespace LoanHub.Backend.UseCases.Mapping.Profiles;
public class ApplicationProfile : Profile
{
	public ApplicationProfile()
	{
		CreateMap<ApplicationEntity, ApplicationDTO>()
			.ForCtorParam(nameof(ApplicationDTO.Status), opt =>
			{
				opt.MapFrom(o => o.Status.Name);
			})
			.ForCtorParam(nameof(ApplicationDTO.FirstName), opt =>
			{
				opt.MapFrom(o => o.PersonalData.FirstName);
			})
			.ForCtorParam(nameof(ApplicationDTO.LastName), opt =>
			{
				opt.MapFrom(o => o.PersonalData.LastName);
			})
			.ForCtorParam(nameof(ApplicationDTO.Age), opt =>
			{
				opt.MapFrom(o => o.PersonalData.Age);
			})
			.ForCtorParam(nameof(ApplicationDTO.Email), opt =>
			{
				opt.MapFrom(o => o.ContactInfo.Email);
			})
			.ForCtorParam(nameof(ApplicationDTO.PhoneNumber), opt =>
			{
				opt.MapFrom(o => o.ContactInfo.PhoneNumber);
			})
			.ForCtorParam(nameof(ApplicationDTO.Address), opt =>
			{
				opt.MapFrom(o => o.ContactInfo.Address);
			})
			.ForCtorParam(nameof(ApplicationDTO.Income), opt =>
			{
				opt.MapFrom(o => o.ApplicantFinancials.Income);
			})
			.ForCtorParam(nameof(ApplicationDTO.Costs), opt =>
			{
				opt.MapFrom(o => o.ApplicantFinancials.Costs);
			})
			.ForCtorParam(nameof(ApplicationDTO.Dependents), opt =>
			{
				opt.MapFrom(o => o.ApplicantFinancials.Dependents);
			})
			.ForCtorParam(nameof(ApplicationDTO.Job), opt =>
			{
				opt.MapFrom(o => o.ApplicantFinancials.Job);
			})
			.ForCtorParam(nameof(ApplicationDTO.Amount), opt =>
			{
				opt.MapFrom(o => o.OfferConditions.Amount);
			})
			.ForCtorParam(nameof(ApplicationDTO.InterestRate), opt =>
			{
				opt.MapFrom(o => o.OfferConditions.InterestRate);
			})
			.ForCtorParam(nameof(ApplicationDTO.Duration), opt =>
			{
				opt.MapFrom(o => o.OfferConditions.Duration);
			});
	}
}
