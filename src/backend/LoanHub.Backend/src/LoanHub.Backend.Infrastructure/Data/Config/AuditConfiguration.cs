namespace LoanHub.Backend.Infrastructure.Data.Config;

public sealed class AuditConfiguration : LoanHubBaseEntityConfiguration<Audit>
{
	public override void Configure(EntityTypeBuilder<Audit> builder)
	{
		base.Configure(builder);

		builder.ToTable("Audit");

		builder.Property(a => a.Method).
			IsRequired();

		builder.Property(a => a.Path).
			IsRequired();

		builder.Property(a => a.Body)
			.IsRequired(false);

		builder.Property(a => a.DurationMs)
			.IsRequired();

		builder.Property(a => a.Error)
			.IsRequired(false);

		builder.Property(a => a.HeadersJson)
			.IsRequired(false);

		builder.Property(a => a.StatusCode).
			IsRequired();

		builder.Property(a => a.ParamsJson)
			.IsRequired();

		builder.Property(a => a.QueryParamsJson)
			.IsRequired();
	}
}