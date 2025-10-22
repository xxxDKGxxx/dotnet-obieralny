using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoanHub.Backend.Infrastructure.Data;

namespace LoanHub.Backend.Infrastructure;
public static class InfrastructureServiceConfig
{
    public static IServiceCollection AddInfrastructureServices(
    this IServiceCollection services,
    ConfigurationManager config,
    ILogger logger)
    {
        string? connectionString = config.GetConnectionString("DefaultConnection");
        Guard.Against.Null(connectionString);
        DatabaseConfig.AddApplicationDbContext(services, connectionString);

        services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>))
               .AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));

        logger.LogInformation("{Project} services registered", "Infrastructure");

        return services;
    }
}
