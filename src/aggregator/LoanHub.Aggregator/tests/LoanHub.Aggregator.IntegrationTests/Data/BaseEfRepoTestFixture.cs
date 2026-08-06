using System.Runtime.CompilerServices;
using LoanHub.Aggregator.Core.Interfaces;
using LoanHub.Aggregator.Infrastructure.Configs;

namespace LoanHub.Aggregator.IntegrationTests.Data;

public abstract class BaseEfRepoTestFixture
{
	protected AppDbContext DbContext;

	protected BaseEfRepoTestFixture()
	{
		var options = CreateNewContextOptions();

		DbContext = new AppDbContext(options);
	}

	protected static DbContextOptions<AppDbContext> CreateNewContextOptions()
	{
		// Create a fresh service provider, and therefore a fresh
		// InMemory database instance.
		var serviceProvider = new ServiceCollection()
			.AddEntityFrameworkInMemoryDatabase()
			.AddSingleton<SoftDeleteInterceptor>()
			.BuildServiceProvider();

		// Create a new options instance telling the context to use an
		// InMemory database and the new service provider.
		var builder = new DbContextOptionsBuilder<AppDbContext>();

		builder.UseInMemoryDatabase("cleanarchitecture")
			   .UseInternalServiceProvider(serviceProvider)
			   .AddInterceptors(serviceProvider.GetRequiredService<SoftDeleteInterceptor>());

		return builder.Options;
	}

	protected EfRepository<Application> GetRepository()
	{
		return new EfRepository<Application>(DbContext);
	}
}