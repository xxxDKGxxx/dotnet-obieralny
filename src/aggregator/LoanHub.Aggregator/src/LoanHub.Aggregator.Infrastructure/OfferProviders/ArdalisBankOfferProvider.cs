using System.Text.Json;
using LoanHub.Aggregator.Core.Interfaces;
using LoanHub.Aggregator.Core.Interfaces.Dtos;

namespace LoanHub.Aggregator.Infrastructure.OfferProviders;

public sealed class ArdalisBankOfferProvider(string apiUrl, HttpClient httpClient) : IOfferProvider
{
	public ApplicationProviderType ProviderType
	{
		get
		{
			return ApplicationProviderType.ArdalisBank;
		}
	}

	private readonly string _apiUrl = apiUrl;

	public async Task<IEnumerable<OfferDto>> ListOffersAsync(decimal amount, uint duration)
	{
		var responseMessage = await httpClient.GetAsync($"{_apiUrl}/offers?Amount={amount}&Duration={duration}");

		if (!responseMessage.IsSuccessStatusCode)
		{
			throw new Exception($"List Offers ArdalisBank Error: "
				+ $"StatusCode: {responseMessage.StatusCode} {await responseMessage.Content.ReadAsStringAsync()}");
		}

		var content = await responseMessage.Content.ReadAsStringAsync();
		var deserialized = JsonSerializer.Deserialize<IEnumerable<OfferDto>>(content);
		return deserialized ?? throw new JsonException("Could not deserialize response");
	}

	public async Task<IEnumerable<CalculatedOfferDto>> ListCalculatedOffersAsync(
		decimal amount,
		uint duration,
		decimal monthlyIncome,
		decimal monthlyCosts,
		int age,
		int dependants)
	{
		var responseMessage = await httpClient.GetAsync($"{_apiUrl}/calculated-offers?Amount={amount}"
	        + $"&Duration={duration}"
	        + $"&MonthlyIncome={monthlyIncome}"
	        + $"&MonthlyCosts={monthlyCosts}"
	        + $"&Age={age}"
	        + $"&Dependants={dependants}");

		if (!responseMessage.IsSuccessStatusCode)
		{
			throw new Exception($"List Calculated Offers ArdalisBank Error: "
				+ $"{await responseMessage.Content.ReadAsStringAsync()}");
		}

		var content = await responseMessage.Content.ReadAsStringAsync();
		var deserialized = JsonSerializer.Deserialize<IEnumerable<CalculatedOfferDto>>(content);
		return deserialized ?? throw new JsonException("Could not deserialize response");
	}
}