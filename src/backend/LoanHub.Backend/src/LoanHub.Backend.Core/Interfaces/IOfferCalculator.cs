using LoanHub.Backend.Core.EntityAggregates.OfferAggregate;
using LoanHub.Backend.Core.EntityAggregates.OfferAggregate.Dto;

namespace LoanHub.Backend.Core.Interfaces;

public interface IOfferCalculator
{
	public OfferConditionsDto Calculate(
		Offer offer,
		decimal requestedAmount,
		uint requestedDuration,
		decimal monthlyIncome,
		decimal monthlyCosts,
		int age,
		int dependants);
}