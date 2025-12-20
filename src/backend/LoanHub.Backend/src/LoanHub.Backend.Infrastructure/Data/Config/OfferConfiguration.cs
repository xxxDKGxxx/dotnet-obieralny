namespace LoanHub.Backend.Infrastructure.Data.Config;

public sealed class OfferConfiguration : LoanHubBaseEntityConfiguration<Offer>
{
	public override void Configure(EntityTypeBuilder<Offer> builder)
	{
		base.Configure(builder);

		builder.ToTable($"{nameof(Offer)}s");

		var amountRange = builder.OwnsOne(o => o.AmountRange);

		amountRange.Property(r => r.Min).
			HasColumnType(DataSchemaConstants.MoneyColumnType)
			.IsRequired();

		amountRange.Property(r => r.Max).
			HasColumnType(DataSchemaConstants.MoneyColumnType)
			.IsRequired();

		ConfigureRange<DurationRange, uint>(builder.OwnsOne(o => o.DurationRange));
		ConfigureRange<InterestRateRange, decimal>(builder.OwnsOne(o => o.InterestRateRange));
		ConfigureRange<ValidRange, DateTime>(builder.OwnsOne(o => o.ValidRange));

		builder.Property(o => o.Description)
			.IsRequired();

		builder.Property(o => o.Title).
			HasMaxLength(DataSchemaConstants.TitleMaxLength).
			IsRequired();
	}

	private static void ConfigureRange<TRange, TRangeValue>(OwnedNavigationBuilder<Offer, TRange> builder)
		where TRangeValue : IComparable<TRangeValue>
		where TRange : Range<TRangeValue>
	{
		builder.Property(r => r.Min)
			.IsRequired();

		builder.Property(r => r.Max)
			.IsRequired();
	}
}