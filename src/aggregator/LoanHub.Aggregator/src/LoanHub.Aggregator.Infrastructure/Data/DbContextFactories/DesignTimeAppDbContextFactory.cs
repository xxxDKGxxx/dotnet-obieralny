using LoanHub.Aggregator.Infrastructure.Data.DbContexts;

namespace LoanHub.Aggregator.Infrastructure.Data.DbContextFactories;

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

		return new AppDbContext(options);
	}
}