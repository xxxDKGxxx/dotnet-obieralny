using LoanHub.Backend.Core.EntityAggregates.OfferAggregate;

namespace LoanHub.Backend.Infrastructure.Data.Config;

public sealed class OfferConfiguration : IEntityTypeConfiguration<Offer>
{
	public void Configure(EntityTypeBuilder<Offer> builder)
	{
		builder.ToTable($"{nameof(Offer)}s");

		builder.HasKey(o => o.Id);

		builder.HasQueryFilter(o => o.IsDeleted);
	}
}