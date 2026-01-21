namespace LoanHub.Backend.UseCases.Mapping.Profiles;

public class AuditProfile : Profile
{
	public AuditProfile()
	{
		CreateMap<AuditEntity, AuditDto>();
	}
}