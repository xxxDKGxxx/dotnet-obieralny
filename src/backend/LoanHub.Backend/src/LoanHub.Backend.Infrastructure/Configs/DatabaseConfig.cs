using LoanHub.Backend.Infrastructure.Data;
using LoanHub.Backend.Infrastructure.Data.Interceptors;

namespace LoanHub.Backend.Infrastructure.Configs;

public static class DatabaseConfig
{
	public static void AddApplicationDbContext(this IServiceCollection services, string connectionString)
	{
		services.AddSingleton<SoftDeleteInterceptor>();

		services.AddDbContext<AppDbContext>((sp, options) =>
		{
			options.UseSqlServer(connectionString, sqlOptions =>
				{
					sqlOptions.EnableRetryOnFailure(
						maxRetryCount: 5,
						maxRetryDelay: TimeSpan.FromSeconds(30),
						errorNumbersToAdd: null
					);
				})
				.AddInterceptors(sp.GetRequiredService<SoftDeleteInterceptor>());
		});
	}
}