using ArdalisBank.Infrastructure.Data;

namespace ArdalisBank.Infrastructure.Configs;

public static class InfrastructureServicesConfig
{
	public static IServiceCollection AddInfrastructureServices(
		this IServiceCollection services,
		ConfigurationManager config,
		ILogger logger)
	{
		try
		{

			var connectionString = config.GetConnectionString("DefaultConnection");
			Guard.Against.Null(connectionString);
			services.AddApplicationDbContext(connectionString);
		}
		catch
		{
			logger.LogError("Wrong database connection string");
			throw;
		}

		services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>))
			   .AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));

		logger.LogInformation("{Project} services registered", "Infrastructure");

		return services;
	}
}