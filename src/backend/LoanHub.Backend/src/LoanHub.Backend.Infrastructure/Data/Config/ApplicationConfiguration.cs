namespace LoanHub.Backend.Infrastructure.Data.Config;

public sealed class ApplicationConfiguration : LoanHubBaseEntityConfiguration<Application>
{
	public override void Configure(EntityTypeBuilder<Application> builder)
	{
		base.Configure(builder);

		builder.ToTable($"{nameof(Application)}s");

<<<<<<< HEAD
		builder.HasOne<Offer>()
			.WithMany()
			.HasForeignKey(a => a.OfferId)
			.OnDelete(DeleteBehavior.Restrict)
			.IsRequired();

		builder.HasOne<User>()
			.WithMany()
			.HasForeignKey(a => a.UserId)
			.OnDelete(DeleteBehavior.Restrict)
			.IsRequired(false);

		builder.HasQueryFilter(a => a.CreatedAt >= DateTime.UtcNow.AddDays(-10));
=======
		builder.Property(a => a.Description)
			.IsRequired();

		builder.Property(a => a.Title).
			HasMaxLength(DataSchemaConstants.TitleMaxLength).
			IsRequired();
>>>>>>> 7ad7476eeea0e2f4c266a6307f360e22ed70bb07

		builder.Property(a => a.Status)
			.HasConversion(s => s.Value, s => ApplicationStatus.FromValue(s))
			.IsRequired();

<<<<<<< HEAD
		var contactInfo = builder.OwnsOne(a => a.ContactInfo);

		contactInfo.Property(ci => ci.Email)
			.HasMaxLength(DataSchemaConstants.EmailMaxLength)
			.IsRequired();
		contactInfo.Property(ci => ci.Address)
			.HasMaxLength(DataSchemaConstants.AddressMaxLength)
			.IsRequired();
		contactInfo.Property(ci => ci.PhoneNumber)
			.HasMaxLength(DataSchemaConstants.PhoneMaxLength)
			.IsRequired();

		var personalData = builder.OwnsOne(a => a.PersonalData);

		personalData.Property(pd => pd.FirstName)
			.HasMaxLength(DataSchemaConstants.FirstNameMaxLength)
			.IsRequired();
		personalData.Property(pd => pd.LastName)
			.HasMaxLength(DataSchemaConstants.LastNameMaxLength)
			.IsRequired();
		personalData.Property(pd => pd.Age)
			.IsRequired();

		var financials = builder.OwnsOne(a => a.ApplicantFinancials);

		financials.Property(f => f.Income)
			.HasColumnType(DataSchemaConstants.MoneyColumnType)
			.IsRequired();
		financials.Property(f => f.Job)
			.HasMaxLength(DataSchemaConstants.JobMaxLength)
			.IsRequired();
		financials.Property(f => f.Costs)
			.HasColumnType(DataSchemaConstants.MoneyColumnType)
			.IsRequired();
		financials.Property(f => f.Dependents)
			.IsRequired();

		var conditions = builder.OwnsOne(a => a.OfferConditions);

		conditions.Property(c => c.Amount).IsRequired();
		conditions.Property(c => c.InterestRate).IsRequired();
		conditions.Property(c => c.Duration).IsRequired();

		builder.Property(a => a.DocumentId)
			.IsRequired(false);

		builder.HasIndex(a => a.UserId);
		builder.HasIndex(a => a.OfferId);
=======
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
>>>>>>> 7ad7476eeea0e2f4c266a6307f360e22ed70bb07
	}
}