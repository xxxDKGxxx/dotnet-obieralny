namespace LoanHub.Backend.Infrastructure.Configs;

public static class AutoMapperConfig
{
	public static IServiceCollection AddAutoMapperConfigs(this IServiceCollection services)
	{
		services.AddAutoMapper(
			_ =>
			{

			},
			typeof(OfferProfile).Assembly);

		return services;
	}
}