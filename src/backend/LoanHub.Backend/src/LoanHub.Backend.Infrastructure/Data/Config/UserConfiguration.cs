namespace LoanHub.Backend.Infrastructure.Data.Config;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
	public void Configure(EntityTypeBuilder<User> builder)
	{
		builder.ToTable($"{nameof(User)}s");

		builder.HasKey(u => u.Id);
		builder.HasQueryFilter(u => !u.IsDeleted);

		builder.Property(u => u.Email)
			.HasMaxLength(DataSchemaConstants.User.EmailMaxLength)
			.IsRequired();
		builder.HasIndex(u => u.Email).IsUnique();

		builder.Property(u => u.FirstName)
			.HasMaxLength(DataSchemaConstants.User.FirstNameMaxLength)
			.IsRequired();

		builder.Property(u => u.LastName)
			.HasMaxLength(DataSchemaConstants.User.LastNameMaxLength)
			.IsRequired();

		builder.Property(u => u.Role)
			.HasConversion(r => r.Value, r => UserRole.FromValue(r))
			.IsRequired();

		builder.Property(u => u.Address)
			.HasMaxLength(DataSchemaConstants.User.AddressMaxLength)
			.IsRequired(false);

		builder.Property(u => u.Phone)
			.HasMaxLength(DataSchemaConstants.User.PhoneMaxLength)
			.IsRequired(false);

		builder.Property(u => u.Income)
			.HasColumnType(DataSchemaConstants.User.MoneyColumnType)
			.IsRequired(false);

		builder.Property(u => u.Costs)
			.HasColumnType(DataSchemaConstants.User.MoneyColumnType)
			.IsRequired(false);

		builder.Property(u => u.Dependents)
			.IsRequired(false);

		builder.Property(u => u.Job)
			.HasMaxLength(DataSchemaConstants.User.JobMaxLength)
			.IsRequired(false);

		builder.Property(u => u.Age)
			.IsRequired(false);
	}
}