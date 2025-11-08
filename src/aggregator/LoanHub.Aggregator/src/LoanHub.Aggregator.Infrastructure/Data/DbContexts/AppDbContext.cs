namespace LoanHub.Aggregator.Infrastructure.Data.DbContexts;

public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
	}

	public override int SaveChanges()
	{
		var result = SaveChangesAsync().
			GetAwaiter().
			GetResult();

		return result;
	}
}