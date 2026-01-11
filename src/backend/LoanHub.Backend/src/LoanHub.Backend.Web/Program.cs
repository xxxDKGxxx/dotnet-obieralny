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

		try
		{
			builder.Services.AddServiceConfigs(appLogger, builder);

			builder.Services.AddFastEndpoints()
				.SwaggerDocument(o =>
				{
					o.DocumentSettings = s =>
					{
						s.Title = "LoanHub API";
						s.Version = "1";
					};
					o.ShortSchemaNames = true;
					o.MaxEndpointVersion = 1;
				})
				.AddCommandMiddleware(c =>
				{
					c.Register(typeof(CommandLogger<,>));
				});

			var frontendOrigin = builder.Configuration.GetValue<string>("FrontendOrigin")
								 ?? "http://localhost:4200";

			builder.Services.AddCors(options =>
			{
				options.AddPolicy("AllowFrontend", policy =>
				{
					policy.WithOrigins(frontendOrigin).
						AllowAnyHeader().
						AllowAnyMethod().
						AllowCredentials();
				});
			});

			var app = builder.Build();

			app.UseCors("AllowFrontend");

			app.UseAuthentication();
			app.UseAuthorization();

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