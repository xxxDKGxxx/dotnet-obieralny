namespace LoanHub.Aggregator.Infrastructure.Data.Config;

public sealed class ApplicationConfiguration : IEntityTypeConfiguration<Application>
{
	public void Configure(EntityTypeBuilder<Application> builder)
	{
		builder.ToTable($"{nameof(Application)}s");

		builder.HasKey(a => a.Id);

		builder.HasQueryFilter(a => !a.IsDeleted);

		builder.Property(a => a.UserId)
			.IsRequired();

		builder.Property(a => a.ProviderApplicationId)
			.HasMaxLength(DataSchemaConstants.ProviderApplicationIdMaxLength)
			.IsRequired();

		builder.Property(a => a.ProviderType)
			.HasConversion(pt => pt.Value, pt => ApplicationProviderType.FromValue(pt))
			.HasMaxLength(DataSchemaConstants.ProviderTypeMaxLength)
			.IsRequired();

		builder.Property(a => a.CreatedAt)
			.HasConversion<string>()
			.IsRequired();

		builder.Property(a => a.DeletedAt)
			.HasConversion<string>()
			.IsRequired(false);

		builder.Property(a => a.IsDeleted)
			.IsRequired(true);
	}
}