namespace LoanHub.Backend.Infrastructure.Configs;

public static class AutoMapperConfig
{
	public static IServiceCollection AddAutoMapperConfigs(this IServiceCollection services)
	{
		services.AddAutoMapper(cfg =>
			{
				Assembly.GetExecutingAssembly();
			});

		return services;
	}
}