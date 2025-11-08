using ArdalisBank.Web.Configurations;

namespace ArdalisBank.Web;

public sealed class Program
{
	private static async Task Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		var logger = Log.Logger = new LoggerConfiguration()
		  .Enrich.FromLogContext()
		  .WriteTo.Console()
		  .CreateLogger();

		logger.Information("Starting web host");

		builder.AddLoggerConfigs();

		var appLogger = new SerilogLoggerFactory(logger)
			.CreateLogger<Program>();

		try
		{
			builder.Services.AddServiceConfigs(appLogger, builder);

			builder.Services.AddFastEndpoints()
				.SwaggerDocument(o =>
				{
					o.ShortSchemaNames = true;
				})
				.AddCommandMiddleware(c =>
				{
					c.Register(typeof(CommandLogger<,>));
				});

			var app = builder.Build();

			await app.UseAppMiddlewareAndSeedDatabase();

			await app.RunAsync();
		}
		catch (Exception ex)
		{
			logger.Error(ex.Message);
			return;
		}
	}
}