using LoanHub.Backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Design;

namespace LoanHub.Backend.Infrastructure.Data.DbContextFactories;

public sealed class DesignTimeAppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
	public AppDbContext CreateDbContext(string[] args)
	{
		var config = new ConfigurationBuilder()
			.AddEnvironmentVariables()
			.Build();

		var connectionString = config.GetConnectionString("DefaultConnection")
			?? throw new Exception("Connection String was not defined in the environment");

		var options = new DbContextOptionsBuilder<AppDbContext>()
			.UseSqlServer(connectionString)
			.Options;

		return new AppDbContext(options, null);
	}
}