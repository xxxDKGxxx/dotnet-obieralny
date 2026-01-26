using LoanHub.Backend.Infrastructure.Data;
using LoanHub.Backend.Infrastructure.Email;

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
			logger.LogError("Default connection string was not defined in the environment");
			throw;
		}

		services.AddApplicationDbContext(connectionString);
		services.AddAutoMapperConfigs();

		services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>))
				.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));

		services.AddScoped<IOfferCalculator, OfferCalculatorService>();
		services.AddScoped<INotificationService, NotificationService>();
		services.AddScoped<IEmailSender, SendGridEmailSender>();
		services.AddScoped<IUpdateApplicationStatusService, UpdateApplicationStatusService>();

		services.AddAuthenticationServices(config, logger);

		logger.LogInformation("{Project} services registered", "Infrastructure");

		return services;
	}
}