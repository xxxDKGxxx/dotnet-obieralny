using LoanHub.Aggregator.Infrastructure.Data.Config;

namespace LoanHub.Aggregator.Infrastructure.Data.Repositories;

public class EfCounterRepository(AppDbContext dbContext) : RepositoryBase<Counter>(dbContext), ICounterRepository
{
	public async Task IncrementAtomicAsync(CancellationToken cancellationToken = default)
	{
		// These lambdas are not functions, but are converted to expression trees therefor cannot have a body
#pragma warning disable IDE0053
		await DbContext.Set<Counter>()
			.Where(c => c.Id == DataSchemaConstants.CounterId)
			.ExecuteUpdateAsync(s => s.SetProperty(
				p => p.Value,
				p => p.Value + 1
			), cancellationToken);
#pragma warning restore IDE0053
	}
}