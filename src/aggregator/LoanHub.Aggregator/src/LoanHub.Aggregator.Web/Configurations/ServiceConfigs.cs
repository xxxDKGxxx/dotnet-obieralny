using LoanHub.Aggregator.Web.Middleware;

namespace LoanHub.Aggregator.Web.Configurations;

public static class ServiceConfigs
{
	public static IServiceCollection AddServiceConfigs(
		this IServiceCollection services,
		Microsoft.Extensions.Logging.ILogger logger,
		WebApplicationBuilder builder)
	{
		services.AddInfrastructureServices(builder.Configuration, logger);

		var defaultBankUrl = builder.Configuration.GetSection("DefaultBankUrl").Value
							 ?? throw new Exception("Default Bank Url was not defined");

		builder.Services.AddHttpClient<DefaultBankRedirectMiddleware>("DefaultBankRedirectClient", opt =>
		{
			opt.BaseAddress = new Uri(defaultBankUrl);
		});

		services.AddHeaderPropagation(opt =>
		{
			opt.Headers.Add("Authorization");
			opt.Headers.Add("X-Correlation-Id");
		});

		var ardalisBankUrl = builder.Configuration.GetValue<string>("ArdalisBankUrl")
							 ?? throw new Exception("ArdalisBankUrl was not defined");

		services.AddHttpClient<IOfferProvider, ArdalisBankOfferProvider>(opt =>
			{
				opt.BaseAddress = new Uri(ardalisBankUrl);
			}).
			AddHeaderPropagation(opt =>
			{
				opt.Headers.Add("Authorization");
				opt.Headers.Add("X-Correlation-Id");
			});

		return services;
	}
}