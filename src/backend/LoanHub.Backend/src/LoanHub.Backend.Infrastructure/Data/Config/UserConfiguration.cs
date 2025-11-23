using LoanHub.Backend.Core.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders; 

namespace LoanHub.Backend.Infrastructure.Data.Config;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable($"{nameof(User)}s");
        
        builder.HasKey(u => u.Id);
        
        builder.HasQueryFilter(u => !u.IsDeleted);
        
        builder.Property(u => u.Email)
            .HasMaxLength(255)
            .IsRequired();
        builder.HasIndex(u => u.Email).IsUnique();
        
        builder.Property(u => u.FirstName)
            .HasMaxLength(100)
            .IsRequired();
            
        builder.Property(u => u.LastName)
            .HasMaxLength(100)
            .IsRequired();
            
        builder.Property(u => u.PasswordHash)
            .IsRequired();
            
        builder.Property(u => u.Role)
            .HasConversion(r => r.Value, r => UserRole.FromValue(r))
            .IsRequired();
            
        builder.Property(u => u.Address)
            .HasMaxLength(500)
            .IsRequired(false);
            
        builder.Property(u => u.Phone)
            .HasMaxLength(20)
            .IsRequired(false);
            
        builder.Property(u => u.Income)
            .HasColumnType("decimal(18,2)")
            .IsRequired(false);
            
        builder.Property(u => u.Costs)
            .HasColumnType("decimal(18,2)")
            .IsRequired(false);
            
        builder.Property(u => u.Dependents)
            .IsRequired(false);
            
        builder.Property(u => u.Job)
            .HasMaxLength(100)
            .IsRequired(false);
            
        builder.Property(u => u.Age)
            .IsRequired(false);
    }
}