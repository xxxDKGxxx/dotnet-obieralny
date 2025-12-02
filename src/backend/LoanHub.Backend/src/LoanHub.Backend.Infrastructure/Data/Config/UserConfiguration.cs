namespace LoanHub.Backend.Infrastructure.Data.Config;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
	public void Configure(EntityTypeBuilder<User> builder)
	{
		builder.ToTable($"{nameof(User)}s");

		builder.HasKey(u => u.Id);
		builder.HasQueryFilter(u => !u.IsDeleted);

		builder.Property(u => u.Email)
			.HasMaxLength(UserConstants.EmailMaxLength)
			.IsRequired();
		builder.HasIndex(u => u.Email).IsUnique();

		builder.Property(u => u.FirstName)
			.HasMaxLength(UserConstants.FirstNameMaxLength)
			.IsRequired();

		builder.Property(u => u.LastName)
			.HasMaxLength(UserConstants.LastNameMaxLength)
			.IsRequired();

		builder.Property(u => u.Role)
			.HasConversion(r => r.Value, r => UserRole.FromValue(r))
			.IsRequired();

		builder.Property(u => u.Address)
			.HasMaxLength(UserConstants.AddressMaxLength)
			.IsRequired(false);

		builder.Property(u => u.Phone)
			.HasMaxLength(UserConstants.PhoneMaxLength)
			.IsRequired(false);

		builder.Property(u => u.Income)
			.HasColumnType(UserConstants.MoneyColumnType)
			.IsRequired(false);

		builder.Property(u => u.Costs)
			.HasColumnType(UserConstants.MoneyColumnType)
			.IsRequired(false);

		builder.Property(u => u.Dependents)
			.IsRequired(false);

		builder.Property(u => u.Job)
			.HasMaxLength(UserConstants.JobMaxLength)
			.IsRequired(false);

		builder.Property(u => u.Age)
			.IsRequired(false);
	}
}