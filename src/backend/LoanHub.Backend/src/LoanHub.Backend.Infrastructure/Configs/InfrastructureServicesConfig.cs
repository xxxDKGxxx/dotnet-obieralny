using LoanHub.Backend.Infrastructure.Data;

namespace LoanHub.Backend.Infrastructure.Configs;

public static class InfrastructureServicesConfig
{
	public static IServiceCollection AddInfrastructureServices(
		this IServiceCollection services,
		ConfigurationManager config,
		ILogger logger)
	{
		var connectionString = config.GetConnectionString("DefaultConnection");
		_ = Guard.Against.Null(connectionString);
		services.AddApplicationDbContext(connectionString);

		_ = services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>))
			   .AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));

		logger.LogInformation("{Project} services registered", "Infrastructure");

		return services;
	}
}

