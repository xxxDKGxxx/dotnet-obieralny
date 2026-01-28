using CounterEntity = LoanHub.Aggregator.Core.Counter.Counter;

namespace LoanHub.Aggregator.Core.Interfaces;

public interface ICounterRepository : IRepositoryBase<CounterEntity>
{
	public Task IncrementAtomicAsync(CancellationToken cancellationToken = default);
}