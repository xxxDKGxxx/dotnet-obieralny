using LoanHub.Backend.Infrastructure.Configs;

namespace LoanHub.Backend.Web.Configurations;

public static class ServiceConfigs
{
	public static IServiceCollection AddServiceConfigs(this IServiceCollection services, Microsoft.Extensions.Logging.ILogger logger, WebApplicationBuilder builder)
	{
		services.AddInfrastructureServices(builder.Configuration, logger)
			.AddMediatrConfigs()
			.AddAutomapperConfigs();
		logger.LogInformation("{Project} services registered", "Mediatr, AutoMapper");

		return services;
	}
}