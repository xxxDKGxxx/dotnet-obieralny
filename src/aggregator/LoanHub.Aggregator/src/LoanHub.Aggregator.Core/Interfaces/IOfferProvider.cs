using LoanHub.Aggregator.Core.Interfaces.Dtos;

namespace LoanHub.Aggregator.Core.Interfaces;

public interface IOfferProvider
{
	public ApplicationProviderType ProviderType
	{
		get;
	}

	public Task<IEnumerable<ApplicationWithProviderTypeDto>> ListApplicationsAsync(
		int? userId,
		CancellationToken cancellationToken = default);
	public Task<ApplicationWithProviderTypeDto> UpdateStatusAsync(
		int applicationId,
		ApplicationStatus newStatus,
		string? statusChangeMessage,
		CancellationToken cancellationToken = default);
	public Task<ApplicationWithProviderTypeDto> CreateApplicationAsync(
		int OfferId,
		int? UserId,
		decimal Amount,
		uint Duration,
		ApplicantFinancialInfo Financials,
		ApplicantContactInfo Contact,
		ApplicantPersonalInfo PersonalData,
		CancellationToken cancellationToken = default);
	public Task<IEnumerable<OfferWithProviderTypeDto>> ListOffersAsync(
		decimal amount,
		uint duration,
		CancellationToken cancellationToken = default);
	public Task<IEnumerable<CalculatedOfferWithProviderTypeDto>> ListCalculatedOffersAsync(
		decimal amount,
		uint duration,
		decimal monthlyIncome,
		decimal monthlyCosts,
		int age,
		int dependants,
		CancellationToken cancellationToken = default);
	public Task<OfferWithProviderTypeDto> GetOfferByIdAsync(
		int offerId,
		CancellationToken cancellationToken = default);
	public Task<CalculatedOfferWithProviderTypeDto> GetCalculatedOfferByIdAsync(
		int offerId,
		decimal amount,
		uint duration,
		decimal monthlyIncome,
		decimal monthlyCosts,
		int age,
		int dependants,
		CancellationToken cancellationToken = default);
}