namespace LoanHub.Aggregator.UnitTests.Core.ApplicationAggregate;

public sealed class ApplicationProviderTypeTests
{
	[Fact]
	public void ArdalisBank_WhenCreatedAndQueriedByValueAndName_ShouldReturnCorrectInstance()
	{
		var appType = ApplicationProviderType.ArdalisBank;

		appType.ShouldNotBeNull();

		appType.Name.ShouldBe("ArdalisBankProviderType");
		appType.Value.ShouldBe("ArdalisBank");

		var result1 = ApplicationProviderType.FromName("ArdalisBankProviderType");

		result1.ShouldBe(appType);

		var result2 = ApplicationProviderType.FromValue("ArdalisBank");

		result2.ShouldBe(appType);
	}
}