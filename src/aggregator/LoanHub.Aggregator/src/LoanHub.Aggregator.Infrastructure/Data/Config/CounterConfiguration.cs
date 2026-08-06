namespace LoanHub.Aggregator.Infrastructure.Data.Config;

public sealed class CounterConfiguration : IEntityTypeConfiguration<Counter>
{
	public void Configure(EntityTypeBuilder<Counter> builder)
	{
		builder.ToTable($"{nameof(Counter)}s");

		builder.HasKey(x => x.Id);

		builder.Property(x => x.Id)
			.ValueGeneratedNever()
			.IsRequired();

		builder.Property(x => x.Value)
			.IsRequired();

		builder.HasData(new Counter(DataSchemaConstants.CounterId, 0));
	}
}