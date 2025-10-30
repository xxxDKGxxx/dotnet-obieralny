using LoanHub.Backend.Core.Interfaces;
using LoanHub.Backend.Infrastructure.Configs;
using LoanHub.Backend.Infrastructure.Email;

namespace LoanHub.Backend.Web.Configurations;

public static class ServiceConfigs
{
	public static IServiceCollection AddServiceConfigs(this IServiceCollection services, Microsoft.Extensions.Logging.ILogger logger, WebApplicationBuilder builder)
	{
		_ = services.AddInfrastructureServices(builder.Configuration, logger)
			.AddMediatrConfigs();

		logger.LogInformation("{Project} services registered", "Mediatr");

		return services;
	}
}
