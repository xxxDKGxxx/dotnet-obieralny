using LoanHub.Backend.Infrastructure.Data;

namespace LoanHub.Backend.Infrastructure.Configs;

public static class DatabaseConfig
{
	public static void AddApplicationDbContext(this IServiceCollection services, string connectionString)
	{
		_ = services.AddDbContext<AppDbContext>(options =>
		{
			_ = options.UseSqlServer(connectionString);
		});
	}
}
