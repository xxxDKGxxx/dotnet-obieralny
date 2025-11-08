using ArdalisBank.Core.Interfaces;
using ArdalisBank.Core.Services;
using ArdalisBank.UseCases.Mapping;

namespace ArdalisBank.Web.Configurations;

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