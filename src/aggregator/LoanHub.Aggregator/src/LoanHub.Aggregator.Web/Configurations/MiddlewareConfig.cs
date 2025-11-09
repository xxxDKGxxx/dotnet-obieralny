using LoanHub.Aggregator.Infrastructure.Data.DbContexts;
using LoanHub.Aggregator.Web.Middleware;

namespace LoanHub.Aggregator.Web.Configurations;

public static class MiddlewareConfig
{
	public static async Task<IApplicationBuilder> UseAppMiddlewareAndSeedDatabase(this WebApplication app)
	{
		if (app.Environment.IsDevelopment())
		{
			app.UseDeveloperExceptionPage();
			app.UseShowAllServicesMiddleware(); // see https://github.com/ardalis/AspNetCoreStartupServices
		}
		else
		{
			app.UseHsts();
		}

		app.UseMiddleware<DefaultBankRedirectMiddleware>();
		app.UseHttpsRedirection(); // Note this will drop Authorization headers

		await SeedDatabase(app);
		return app;
	}

	private static async Task SeedDatabase(WebApplication app)
	{
		using var scope = app.Services.CreateScope();
		var services = scope.ServiceProvider;

		try
		{
			var context = services.GetRequiredService<AppDbContext>();
			await context.Database.MigrateAsync();
			_ = await context.Database.EnsureCreatedAsync();
			await SeedData.InitializeAsync(context);
		}
		catch (Exception ex)
		{
			var logger = services.GetRequiredService<ILogger<Program>>();
			logger.LogError(ex, "An error occurred seeding the DB. {exceptionMessage}", ex.Message);
		}
	}
}