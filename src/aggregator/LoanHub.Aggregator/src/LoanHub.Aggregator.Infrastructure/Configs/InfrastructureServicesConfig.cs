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

		logger.LogInformation("{Project} services registered", "Infrastructure");

		return services;
	}
}