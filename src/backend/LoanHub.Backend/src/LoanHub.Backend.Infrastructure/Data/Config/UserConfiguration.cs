using LoanHub.Backend.Core.UserAggregate;

namespace LoanHub.Backend.Infrastructure.Data.Config;

public sealed class UserConfiguration : LoanHubBaseEntityConfiguration<User>
{
	public override void Configure(EntityTypeBuilder<User> builder)
	{
		base.Configure(builder);

		builder.ToTable($"{nameof(User)}s");

		builder.Property(u => u.Email)
			.HasMaxLength(DataSchemaConstants.EmailMaxLength)
			.IsRequired();

		builder.HasIndex(u => u.Email)
			.IsUnique();

		builder.Property(u => u.FirstName)
			.HasMaxLength(DataSchemaConstants.FirstNameMaxLength)
			.IsRequired();

		builder.Property(u => u.LastName)
			.HasMaxLength(DataSchemaConstants.LastNameMaxLength)
			.IsRequired();

		builder.Property(u => u.Role)
			.HasConversion(r => r.Value, r => UserRole.FromValue(r))
			.IsRequired();

		builder.Property(u => u.Address)
			.HasMaxLength(DataSchemaConstants.AddressMaxLength)
			.IsRequired(false);

		builder.Property(u => u.Phone)
			.HasMaxLength(DataSchemaConstants.PhoneMaxLength)
			.IsRequired(false);

		builder.Property(u => u.Income)
			.HasColumnType(DataSchemaConstants.MoneyColumnType)
			.IsRequired(false);

		builder.Property(u => u.Costs)
			.HasColumnType(DataSchemaConstants.MoneyColumnType)
			.IsRequired(false);

		builder.Property(u => u.Dependents)
			.IsRequired(false);

		builder.Property(u => u.Job)
			.HasMaxLength(DataSchemaConstants.JobMaxLength)
			.IsRequired(false);

		builder.Property(u => u.Age)
			.IsRequired(false);
	}
}