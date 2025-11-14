using LoanHub.Backend.Infrastructure.Data;

namespace LoanHub.Backend.Infrastructure.Configs;

public static class InfrastructureServicesConfig
{
	public static IServiceCollection AddInfrastructureServices(
		this IServiceCollection services,
		ConfigurationManager config,
		ILogger logger)
	{
		string? connectionString;
		try
		{
			connectionString = config.GetConnectionString("DefaultConnection");
			Guard.Against.Null(connectionString);
		}
		catch
		{
			logger.LogError("Wrong database connection string");
			throw;
		}

		services.AddApplicationDbContext(connectionString);
		services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>))
			   .AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));

		logger.LogInformation("{Project} services registered", "Infrastructure");

		return services;
	}
}