namespace LoanHub.Backend.Infrastructure.Data.Config;

public sealed class ApplicationConfiguration : LoanHubBaseEntityConfiguration<Application>
{
	public override void Configure(EntityTypeBuilder<Application> builder)
	{
		base.Configure(builder);

		builder.ToTable($"{nameof(Application)}s");

		builder.Property(a => a.Status)
			.HasConversion(s => s.Value, s => ApplicationStatus.FromValue(s))
			.IsRequired();

		builder.Property(a => a.Duration).IsRequired();
		builder.Property(a => a.InterestRate).IsRequired();
		builder.Property(a => a.Amount).IsRequired();

		builder.HasOne<Offer>()
			.WithMany()
			.HasForeignKey(a => a.OfferId)
			.OnDelete(DeleteBehavior.Restrict);

		builder.HasOne<User>()
			.WithMany()
			.HasForeignKey(a => a.UserId)
			.OnDelete(DeleteBehavior.Restrict)
			.IsRequired();

		builder.HasOne<User>()
			   .WithMany()
			   .HasForeignKey(a => a.BankEmployeeId)
			   .OnDelete(DeleteBehavior.SetNull);

		builder.HasIndex(a => a.UserId);
		builder.HasIndex(a => a.OfferId);
		builder.HasIndex(a => a.BankEmployeeId);
	}
}