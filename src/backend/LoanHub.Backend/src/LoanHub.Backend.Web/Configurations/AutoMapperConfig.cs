using LoanHub.Backend.Core.Interfaces;
using LoanHub.Backend.Core.Services;
using LoanHub.Backend.UseCases.Mapping;

namespace LoanHub.Backend.Web.Configurations;

public static class AutoMapperConfig
{
	public static IServiceCollection AddAutomapperConfigs(this IServiceCollection services)
	{
		services.AddAutoMapper(config =>
		{
			config.AddProfile<ApplicationMappingProfile>();
		},
		typeof(ApplicationMappingProfile).Assembly
		);

		services.AddScoped<IMapperService, MapperService>();

		return services;
	}
}