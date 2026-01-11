namespace LoanHub.Aggregator.Web.Configurations;

public static class ServiceConfigs
{
	public static IServiceCollection AddServiceConfigs(
		this IServiceCollection services,
		Microsoft.Extensions.Logging.ILogger logger,
		WebApplicationBuilder builder)
	{
		services.AddInfrastructureServices(builder.Configuration, logger);

		return services;
	}
}