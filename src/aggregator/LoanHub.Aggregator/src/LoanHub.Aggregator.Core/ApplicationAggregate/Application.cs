using LoanHub.Aggregator.Core.Interfaces;

namespace LoanHub.Aggregator.Core.ApplicationAggregate;

public sealed class Application(
	int userId,
	ApplicationProviderType providerType,
	string providerApplicationId) :
	LoanHubEntityBase,
	IAggregateRoot
{
	public int UserId { get; private set; } = userId;
	public ApplicationProviderType ProviderType { get; private set; } = providerType;
	public string ProviderApplicationId { get; private set; } = providerApplicationId;
}