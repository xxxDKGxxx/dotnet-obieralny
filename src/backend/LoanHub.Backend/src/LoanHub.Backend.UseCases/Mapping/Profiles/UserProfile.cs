namespace LoanHub.Backend.UseCases.Mapping.Profiles;

public class UserProfile : Profile
{
	public UserProfile()
	{
		CreateMap<UserEntity, UserProfileDto>()
			.ForMember(dest => dest.Role, opt =>
			{
				opt.MapFrom(src => src.Role.Value);
			});
	}
}