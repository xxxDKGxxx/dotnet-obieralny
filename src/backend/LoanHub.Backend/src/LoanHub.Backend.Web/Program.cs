using LoanHub.Backend.Web.Configurations;

namespace LoanHub.Backend.Web;

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

		// wire up commands
		//builder.Services.AddTransient<ICommandHandler<CreateContributorCommand2,Result<int>>, CreateContributorCommandHandler2>();

		var app = builder.Build();

		await app.UseAppMiddlewareAndSeedDatabase();

		await app.RunAsync();
	}
}
