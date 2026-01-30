using Ardalis.ListStartupServices;
using LoanHub.Backend.Infrastructure.Data;
using LoanHub.Backend.Web.Middleware;
using Microsoft.EntityFrameworkCore;

namespace LoanHub.Backend.Web.Configurations;

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
			app.UseDefaultExceptionHandler(); // from FastEndpoints
			app.UseHsts();
		}

		app.UseFastEndpoints(
				c =>
				{
					c.Endpoints.RoutePrefix = "api";
					c.Versioning.Prefix = "v";
					c.Versioning.PrependToRoute = true;
				})
			.UseSwaggerGen(); // Includes AddFileServer and static files middleware

		app.UseMiddleware<AuditMiddleware>();

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
			if (app.Environment.IsDevelopment())
			{
				await SeedData.InitializeTestAsync(context);
			}
			await SeedData.InitializeAsync(context);
		}
		catch (Exception ex)
		{
			var logger = services.GetRequiredService<ILogger<Program>>();
			logger.LogError(ex, "An error occurred seeding the DB. {exceptionMessage}", ex.Message);
		}
	}
}