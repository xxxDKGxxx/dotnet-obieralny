using LoanHub.Aggregator.Infrastructure.Data;
using LoanHub.Aggregator.Infrastructure.Data.DbContexts;

namespace LoanHub.Aggregator.IntegrationTests.Data;

public abstract class BaseEfRepoTestFixture
{
	protected AppDbContext _dbContext;

	protected BaseEfRepoTestFixture()
	{
		var options = CreateNewContextOptions();

		_dbContext = new AppDbContext(options);
	}

	protected static DbContextOptions<AppDbContext> CreateNewContextOptions()
	{
		// Create a fresh service provider, and therefore a fresh
		// InMemory database instance.
		var serviceProvider = new ServiceCollection()
			.AddEntityFrameworkInMemoryDatabase()
			.BuildServiceProvider();

		// Create a new options instance telling the context to use an
		// InMemory database and the new service provider.
		var builder = new DbContextOptionsBuilder<AppDbContext>();
		builder.UseInMemoryDatabase("cleanarchitecture")
			   .UseInternalServiceProvider(serviceProvider);

		return builder.Options;
	}

}