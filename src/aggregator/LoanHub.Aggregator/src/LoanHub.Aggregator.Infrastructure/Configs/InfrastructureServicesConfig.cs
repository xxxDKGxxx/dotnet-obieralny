using LoanHub.Aggregator.Infrastructure.Data;
using LoanHub.Aggregator.Infrastructure.OfferProviders;

namespace LoanHub.Aggregator.Infrastructure.Configs;

public static class InfrastructureServicesConfig
{
	public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
		ConfigurationManager config,
		ILogger logger)
	{
		var connectionString = config.GetConnectionString("DefaultConnection");

		if (connectionString is not null)
		{
			services.AddApplicationDbContext(connectionString);
		}

		services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>))
			   .AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));

		var ardalisBankUrl = config.GetValue<string>("ArdalisBankUrl")
			?? throw new Exception("ArdalisBankUrl was not defined");

		services.AddScoped<IOfferProvider, ArdalisBankOfferProvider>(sp =>
		{
			return new ArdalisBankOfferProvider(
						ardalisBankUrl,
						sp.GetService<HttpClient>() ?? throw new Exception("Could not inject HttpClient"));
		});

		logger.LogInformation("{Project} services registered", "Infrastructure");

		return services;
	}
}