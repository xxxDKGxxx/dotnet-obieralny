using System.Net.Http.Json;
using LoanHub.Aggregator.Core.Interfaces.Dtos;

namespace LoanHub.Aggregator.Infrastructure.OfferProviders;

public sealed record OfferDto(
	int Id,
	string Title,
	string Description,
	decimal MinAmount,
	decimal MaxAmount,
	uint MinDuration,
	uint MaxDuration,
	decimal MinInterestRate,
	decimal MaxInterestRate,
	DateTime ValidFrom,
	DateTime ValidTo
);

public sealed record CalculatedOfferDto(
	int Id,
	string Title,
	string Description,
	decimal Amount,
	uint Duration,
	decimal InterestRate,
	DateTime ValidFrom,
	DateTime ValidTo
);

public sealed record ApplicationDto(
	int Id,
	int OfferId,
	int? UserId,
	string Status,
	ApplicantContactInfo ContactInfo,
	ApplicantFinancialInfo ApplicantFinancials,
	ApplicantPersonalInfo PersonalData,
	OfferConditions OfferConditions,
	string? DocumentId);

public sealed record PostApplicationRequest(
	int OfferId,
	int? UserId,
	decimal Amount,
	uint Duration,
	ApplicantFinancialInfo Financials,
	ApplicantContactInfo Contact,
	ApplicantPersonalInfo PersonalData);

public record UpdateApplicationStatusRequest(string NewStatus);

public sealed class ArdalisBankOfferProvider(HttpClient httpClient) : IOfferProvider
{
	public ApplicationProviderType ProviderType
	{
		get
		{
			return ApplicationProviderType.ArdalisBank;
		}
	}

	private readonly JsonSerializerOptions _jsonSerializerOptions = new()
	{
		PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
	};

	public async Task<ApplicationWithProviderTypeDto> UpdateStatusAsync(int applicationId, ApplicationStatus newStatus)
	{
		var responseMessage = await httpClient.PutAsJsonAsync(
			$"applications/{applicationId}/status",
			new UpdateApplicationStatusRequest(newStatus.Value));

		if (!responseMessage.IsSuccessStatusCode)
		{
			throw new Exception($"Update Application Status ArdalisBank Error: "
								+ $"StatusCode: {responseMessage.StatusCode} "
								+ $"{await responseMessage.Content.ReadAsStringAsync()}");
		}

		var content = await responseMessage.Content.ReadAsStringAsync();
		var deserialized = JsonSerializer.Deserialize<ApplicationDto>(
							   content,
							   _jsonSerializerOptions)
						   ?? throw new JsonException("Could not deserialize response");

		var result = new ApplicationWithProviderTypeDto(
			deserialized.Id,
			deserialized.OfferId,
			deserialized.UserId,
			deserialized.Status,
			deserialized.ContactInfo,
			deserialized.ApplicantFinancials,
			deserialized.PersonalData,
			deserialized.OfferConditions,
			deserialized.DocumentId,
			ProviderType.Value);

		return result;
	}

	public async Task<ApplicationWithProviderTypeDto> CreateApplicationAsync(
		int OfferId,
		int? UserId,
		decimal Amount,
		uint Duration,
		ApplicantFinancialInfo Financials,
		ApplicantContactInfo Contact,
		ApplicantPersonalInfo PersonalData)
	{
		var responseMessage = await httpClient.PostAsJsonAsync($"applications",
			new PostApplicationRequest(
				OfferId,
				UserId,
				Amount,
				Duration,
				Financials,
				Contact,
				PersonalData));

		if (!responseMessage.IsSuccessStatusCode)
		{
			throw new Exception($"Post Application ArdalisBank Error: "
								+ $"StatusCode: {responseMessage.StatusCode} {await responseMessage.Content.ReadAsStringAsync()}");
		}

		var content = await responseMessage.Content.ReadAsStringAsync();
		var deserialized = JsonSerializer.Deserialize<ApplicationDto>(
							   content,
							   _jsonSerializerOptions)
						   ?? throw new JsonException("Could not deserialize response");

		var result = new ApplicationWithProviderTypeDto(
			deserialized.Id,
			deserialized.OfferId,
			deserialized.UserId,
			deserialized.Status,
			deserialized.ContactInfo,
			deserialized.ApplicantFinancials,
			deserialized.PersonalData,
			deserialized.OfferConditions,
			deserialized.DocumentId,
			ProviderType.Value);

		return result;
	}

	public async Task<IEnumerable<OfferWithProviderTypeDto>> ListOffersAsync(decimal amount, uint duration)
	{
		var responseMessage = await httpClient.GetAsync($"offers?Amount={amount}&Duration={duration}");

		if (!responseMessage.IsSuccessStatusCode)
		{
			throw new Exception($"List Offers ArdalisBank Error: "
				+ $"StatusCode: {responseMessage.StatusCode} {await responseMessage.Content.ReadAsStringAsync()}");
		}

		var content = await responseMessage.Content.ReadAsStringAsync();
		var deserialized = JsonSerializer.Deserialize<IEnumerable<OfferDto>>(
		   content,
		   _jsonSerializerOptions)
				?? throw new JsonException("Could not deserialize response");

		var result = deserialized.Select(o =>
		{
			return new OfferWithProviderTypeDto(
						o.Id,
						o.Title,
						o.Description,
						o.MinAmount,
						o.MaxAmount,
						o.MinDuration,
						o.MaxDuration,
						o.MinInterestRate,
						o.MaxInterestRate,
						o.ValidFrom,
						o.ValidTo,
						ProviderType.Value);
		});

		return result;
	}

	public async Task<IEnumerable<CalculatedOfferWithProviderTypeDto>> ListCalculatedOffersAsync(
		decimal amount,
		uint duration,
		decimal monthlyIncome,
		decimal monthlyCosts,
		int age,
		int dependants)
	{
		var responseMessage = await httpClient.GetAsync($"calculated-offers?Amount={amount}"
			+ $"&Duration={duration}"
			+ $"&MonthlyIncome={monthlyIncome}"
			+ $"&MonthlyCosts={monthlyCosts}"
			+ $"&Age={age}"
			+ $"&Dependants={dependants}");

		if (!responseMessage.IsSuccessStatusCode)
		{
			throw new Exception($"List Calculated Offers ArdalisBank Error: "
								+ $"Status Code: {responseMessage.StatusCode}"
								+ $"{await responseMessage.Content.ReadAsStringAsync()}");
		}

		var content = await responseMessage.Content.ReadAsStringAsync();
		var deserialized = JsonSerializer.Deserialize<IEnumerable<CalculatedOfferDto>>(
							   content,
							   _jsonSerializerOptions)
						   ?? throw new JsonException("Could not deserialize response");

		var result = deserialized.Select(o =>
		{
			return new CalculatedOfferWithProviderTypeDto(
						o.Id,
						o.Title,
						o.Description,
						o.Amount,
						o.Duration,
						o.InterestRate,
						o.ValidFrom,
						o.ValidTo,
						ProviderType.Value);
		});

		return result;
	}

	public async Task<OfferWithProviderTypeDto> GetOfferByIdAsync(int offerId)
	{
		var responseMessage = await httpClient.GetAsync($"offers/{offerId}");

		if (!responseMessage.IsSuccessStatusCode)
		{
			throw new Exception($"Get offer by id ArdalisBank Error: "
								+ $"StatusCode: {responseMessage.StatusCode} "
								+ $"{await responseMessage.Content.ReadAsStringAsync()}");
		}

		var content = await responseMessage.Content.ReadAsStringAsync();
		var deserialized = JsonSerializer.Deserialize<OfferDto>(content, _jsonSerializerOptions)
						   ?? throw new JsonException("Could not deserialize response");

		var result = new OfferWithProviderTypeDto(
			deserialized.Id,
			deserialized.Title,
			deserialized.Description,
			deserialized.MinAmount,
			deserialized.MaxAmount,
			deserialized.MinDuration,
			deserialized.MaxDuration,
			deserialized.MinInterestRate,
			deserialized.MaxInterestRate,
			deserialized.ValidFrom,
			deserialized.ValidTo,
			ProviderType.Value);

		return result;
	}

	public async Task<CalculatedOfferWithProviderTypeDto> GetCalculatedOfferByIdAsync(
		int offerId,
		decimal amount,
		uint duration,
		decimal monthlyIncome,
		decimal monthlyCosts,
		int age,
		int dependants)
	{
		var responseMessage = await httpClient.GetAsync($"calculated-offers/{offerId}?Amount={amount}"
														+ $"&Duration={duration}"
														+ $"&MonthlyIncome={monthlyIncome}"
														+ $"&MonthlyCosts={monthlyCosts}"
														+ $"&Age={age}"
														+ $"&Dependants={dependants}");

		if (!responseMessage.IsSuccessStatusCode)
		{
			throw new Exception($"Get Calculated Offers by id ArdalisBank Error: "
								+ $"Status Code: {responseMessage.StatusCode}"
								+ $"{await responseMessage.Content.ReadAsStringAsync()}");
		}

		var content = await responseMessage.Content.ReadAsStringAsync();
		var deserialized = JsonSerializer.Deserialize<CalculatedOfferDto>(
							   content,
							   _jsonSerializerOptions)
						   ?? throw new JsonException("Could not deserialize response");

		var result = new CalculatedOfferWithProviderTypeDto(
			deserialized.Id,
			deserialized.Title,
			deserialized.Description,
			deserialized.Amount,
			deserialized.Duration,
			deserialized.InterestRate,
			deserialized.ValidFrom,
			deserialized.ValidTo,
			ProviderType.Value);

		return result;
	}
}