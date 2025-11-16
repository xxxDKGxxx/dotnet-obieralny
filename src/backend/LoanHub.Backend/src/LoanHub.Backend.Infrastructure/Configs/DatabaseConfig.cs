namespace LoanHub.Backend.Infrastructure.Configs;

public static class DatabaseConfig
{
	public static void AddApplicationDbContext(this IServiceCollection services, string connectionString)
	{
		services.AddSingleton<SoftDeleteInterceptor>();

		services.AddDbContext<AppDbContext>(options =>
		{
			options.UseSqlServer(connectionString);
		});
	}
}