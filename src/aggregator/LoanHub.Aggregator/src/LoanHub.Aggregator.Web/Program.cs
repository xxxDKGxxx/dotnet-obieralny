using LoanHub.Aggregator.Web.Configurations;

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

		builder.Services.AddServiceConfigs(appLogger, builder);

		builder.Services.AddFastEndpoints()
			.SwaggerDocument(o =>
			{
				o.ShortSchemaNames = true;
				o.EnableJWTBearerAuth = true;
				o.DocumentSettings = s =>
				{
					s.Title = "Moje API";
					s.Version = "v1";

					s.AddSecurity("Bearer",
						new NSwag.OpenApiSecurityScheme
						{
							Type = NSwag.OpenApiSecuritySchemeType.ApiKey,
							Name = "Authorization",
							In = NSwag.OpenApiSecurityApiKeyLocation.Header,
							Description = "Wpisz: Bearer {Twój_Token}"
						});

					s.OperationProcessors.Add(new OperationSecurityScopeProcessor("Bearer"));
				};
			});

		builder.Services.AddCors(options =>
		{
			options.AddPolicy("AllowAll", policyBuilder =>
			{
				policyBuilder.AllowAnyHeader();
				policyBuilder.AllowAnyMethod();
				policyBuilder.AllowAnyOrigin();
				policyBuilder.WithExposedHeaders("Content-Disposition");
			});
		});

		var app = builder.Build();

		app.UseCors("AllowAll");

		app.UseAuthentication();
		app.UseAuthorization();

		await app.UseAppMiddlewareAndSeedDatabase();
		await app.RunAsync();
	}
}