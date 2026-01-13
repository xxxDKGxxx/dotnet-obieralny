using LoanHub.Aggregator.Core.ApplicationAggregate;
using LoanHub.Aggregator.Core.Interfaces.Dtos;

namespace LoanHub.Aggregator.Core.Interfaces;

public interface IOfferProvider
{
	public ApplicationProviderType ProviderType
	{
		get;
	}
	public Task<IEnumerable<OfferDto>> ListOffersAsync(decimal amount, uint duration);
	public Task<IEnumerable<CalculatedOfferDto>> ListCalculatedOffersAsync(
		decimal amount,
		uint duration,
		decimal monthlyIncome,
		decimal monthlyCosts,
		int age,
		int dependants);
	public Task<ApplicationDTO> GetApplicationByIdAsync(int applicationId);
}