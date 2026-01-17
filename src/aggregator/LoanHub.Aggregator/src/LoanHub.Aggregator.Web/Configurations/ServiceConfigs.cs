using LoanHub.Aggregator.Core.Interfaces;

namespace LoanHub.Aggregator.Web.Configurations;

public static class ServiceConfigs
{
	public static IServiceCollection AddServiceConfigs(
		this IServiceCollection services,
		Microsoft.Extensions.Logging.ILogger logger,
		WebApplicationBuilder builder)
	{
		services.AddInfrastructureServices(builder.Configuration, logger);

		services.AddHeaderPropagation(opt =>
		{
			opt.Headers.Add("Authorization");
			opt.Headers.Add("X-Correlation-Id");
		});

		services.AddHttpClient<IOfferProvider>().
			AddHeaderPropagation();

		return services;
	}
}