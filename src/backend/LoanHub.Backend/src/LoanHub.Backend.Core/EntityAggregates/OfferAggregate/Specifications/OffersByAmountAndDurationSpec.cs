namespace LoanHub.Backend.Core.EntityAggregates.OfferAggregate.Specifications;

public class OffersByAmountAndDurationSpec : Specification<Offer>
{
	public OffersByAmountAndDurationSpec(decimal amount, uint duration)
	{
		Query.Where(
			o => o.AmountRange.Min <= amount
			&& amount <= o.AmountRange.Max
			&& o.DurationRange.Min <= duration
			&& duration <= o.DurationRange.Max);
	}
}