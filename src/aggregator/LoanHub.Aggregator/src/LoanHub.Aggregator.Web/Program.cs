using LoanHub.Aggregator.Core.ApplicationAggregate;
using LoanHub.Aggregator.Infrastructure.Configs;
using LoanHub.Aggregator.Web.Configurations;
using LoanHub.Aggregator.Web.Middleware;

namespace LoanHub.Aggregator.Web;

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

		builder.Services.AddInfrastructureServices(builder.Configuration, appLogger);
		builder.Services.AddHttpClient<DefaultBankRedirectMiddleware>("DefaultBankRedirectClient");

		var app = builder.Build();

		await app.UseAppMiddlewareAndSeedDatabase();
		await app.RunAsync();
	}
}