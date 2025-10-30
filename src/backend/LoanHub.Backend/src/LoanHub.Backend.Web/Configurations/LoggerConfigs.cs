namespace LoanHub.Backend.Web.Configurations;

public static class LoggerConfigs
{
	public static WebApplicationBuilder AddLoggerConfigs(this WebApplicationBuilder builder)
	{

		_ = builder.Host.UseSerilog((_, config) =>
		{
			_ = config.ReadFrom.Configuration(builder.Configuration);
		});

		return builder;
	}
}
