namespace LoanHub.Backend.UseCases.Mapping;

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