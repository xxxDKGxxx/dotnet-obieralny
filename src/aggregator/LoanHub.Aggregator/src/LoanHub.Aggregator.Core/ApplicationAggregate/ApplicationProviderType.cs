namespace LoanHub.Aggregator.Core.ApplicationAggregate;

public class ApplicationProviderType(
	string name,
	string value) :
	SmartEnum<ApplicationProviderType, string>(name, value)
{
	public static readonly ApplicationProviderType ArdalisBank = new ArdalisBankProviderType();

	private sealed class ArdalisBankProviderType() :
		ApplicationProviderType(nameof(ArdalisBankProviderType), nameof(ArdalisBank))
	{
	}
}