namespace LoanHub.Backend.Core.EntityAggregates.OfferAggregate.Specifications;

public sealed class OfferByIdSpec : SingleResultSpecification<Offer>
{
	public OfferByIdSpec(int offerId)
	{
		Query.Where(o => o.Id == offerId);
	}
}