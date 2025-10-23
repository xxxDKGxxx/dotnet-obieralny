using LoanHub.Backend.Infrastructure.Data;

namespace LoanHub.Backend.Infrastructure.Configs;

public static class DatabaseConfig
{
	public static void AddApplicationDbContext(this IServiceCollection services, string connectionString)
	{
		services.AddDbContext<AppDbContext>(options
			=>
		{
			options.UseSqlite(connectionString);
		});
	}

}
