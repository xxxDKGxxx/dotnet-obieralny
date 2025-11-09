using LoanHub.Aggregator.Infrastructure.Data;
using LoanHub.Aggregator.Infrastructure.Data.DbContexts;
using LoanHub.Aggregator.Infrastructure.Data.Interceptors;

namespace LoanHub.Aggregator.Infrastructure.Configs;

public static class DatabaseConfig
{
	public static void AddApplicationDbContext(this IServiceCollection services, string connectionString)
	{
		services.AddSingleton<SoftDeleteInterceptor>();

		services.AddDbContext<AppDbContext>((sp, options) =>
		{
			options.UseSqlServer(connectionString)
				.AddInterceptors(sp.GetRequiredService<SoftDeleteInterceptor>());
		});
	}
}