namespace LoanHub.Aggregator.Core.Counter;

public sealed class Counter(int id, int value) : IAggregateRoot
{
	public int Id { get; private set; } = id;
	public int Value { get; private set; } = value;
}