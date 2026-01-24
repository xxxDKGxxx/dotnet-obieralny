using LoanHub.Aggregator.Core.Interfaces.Dtos;

namespace LoanHub.Aggregator.Core.Interfaces;

public interface IOfferProvider
{
	public ApplicationProviderType ProviderType
	{
		get;
	}

	public Task<IEnumerable<ApplicationWithProviderTypeDto>> ListApplicationsAsync(int? userId);
	public Task<ApplicationWithProviderTypeDto> CreateApplicationAsync(
		int OfferId,
		int? UserId,
		decimal Amount,
		uint Duration,
		ApplicantFinancialInfo Financials,
		ApplicantContactInfo Contact,
		ApplicantPersonalInfo PersonalData);
	public Task<IEnumerable<OfferWithProviderTypeDto>> ListOffersAsync(decimal amount, uint duration);
	public Task<IEnumerable<CalculatedOfferWithProviderTypeDto>> ListCalculatedOffersAsync(
		decimal amount,
		uint duration,
		decimal monthlyIncome,
		decimal monthlyCosts,
		int age,
		int dependants);
	public Task<OfferWithProviderTypeDto> GetOfferByIdAsync(int offerId);
	public Task<CalculatedOfferWithProviderTypeDto> GetCalculatedOfferByIdAsync(
		int offerId,
		decimal amount,
		uint duration,
		decimal monthlyIncome,
		decimal monthlyCosts,
		int age,
		int dependants);
}