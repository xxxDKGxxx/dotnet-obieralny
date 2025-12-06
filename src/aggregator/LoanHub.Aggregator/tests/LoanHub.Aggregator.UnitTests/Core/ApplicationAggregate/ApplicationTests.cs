namespace LoanHub.Aggregator.UnitTests.Core.ApplicationAggregate;

public class ApplicationTests
{
	[Fact]
	public void Application_WhenCreated_ShouldHaveCorrectFields()
	{
		const int userId = 1;
		const string providerId = "69";

		var appType = ApplicationProviderType.ArdalisBank;

		var app = new Application(
			userId,
			appType,
			providerId);

		app.UserId.ShouldBe(userId);
		app.ProviderType.ShouldBe(appType);
		app.ProviderApplicationId.ShouldBe(providerId);
		app.Id.ShouldBe(0);
	}
}