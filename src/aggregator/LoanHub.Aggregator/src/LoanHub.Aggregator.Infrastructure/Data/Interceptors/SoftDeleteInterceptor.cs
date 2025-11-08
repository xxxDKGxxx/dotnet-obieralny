namespace LoanHub.Aggregator.Infrastructure.Data.Interceptors;

public sealed class SoftDeleteInterceptor : SaveChangesInterceptor
{
	public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
		DbContextEventData eventData,
		InterceptionResult<int> result,
		CancellationToken cancellationToken = new CancellationToken())
	{
		var entries = eventData.Context?.ChangeTracker.Entries()
			.Where(e =>
			{
				return e is { State: EntityState.Deleted, Entity: LoanHubEntityBase };
			})
			.ToList();

		if (entries is null)
		{
			return base.SavingChangesAsync(eventData, result, cancellationToken);
		}

		foreach (var entityEntry in entries)
		{
			entityEntry.State = EntityState.Modified;

			var loanHubEntity = entityEntry.Entity as LoanHubEntityBase;

			loanHubEntity?.Delete();
		}

		return base.SavingChangesAsync(eventData, result, cancellationToken);
	}
}