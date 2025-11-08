namespace ArdalisBank.Infrastructure.Data;

// inherit from Ardalis.Specification type
public sealed class EfRepository<T>(AppDbContext dbContext) :
	RepositoryBase<T>(dbContext),
	IReadRepository<T>,
	IRepository<T> where T : class, IAggregateRoot
{
}