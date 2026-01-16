using LoanHub.Backend.IntegrationTests.Data;

namespace LoanHub.Backend.IntegrationTests.Core.ApplicationAggregate.Specifications;
public class ApplicationByIdSpecIntegrationTests : BaseEfRepoTestFixture
{
	private readonly EfRepository<Application> _applicationRepository;

	public ApplicationByIdSpecIntegrationTests()
	{
		_applicationRepository = new EfRepository<Application>(_dbContext);
	}

	[Fact]
	public async Task Spec_ShouldReturnCorrectApplication_WhenIdMatches()
	{
		var app1 = CreateTestApplication("App-001", userId: 10);
		var app2 = CreateTestApplication("App-002", userId: 15);
		var app3 = CreateTestApplication("App-003", userId: 10);

		await _dbContext.Set<Application>().AddRangeAsync(app1, app2, app3);
		await _dbContext.SaveChangesAsync();

		var targetId = app2.Id;
		var spec = new ApplicationByIdSpec(targetId);

		var result = await _applicationRepository.FirstOrDefaultAsync(spec, CancellationToken.None);

		result.ShouldNotBeNull();
		result.Id.ShouldBe(targetId);
		result.OfferId.ShouldBe(app2.OfferId);
		result.UserId.ShouldBe(app2.UserId);
		result.Status.ShouldBe(app2.Status);
		result.PersonalData.ShouldBe(app2.PersonalData);
	}

	[Fact]
	public async Task Spec_ShouldReturnNull_WhenNoApplicationWithGivenId()
	{
		var app1 = CreateTestApplication("App-001", userId: 5);
		var app2 = CreateTestApplication("App-002", userId: 7);

		await _dbContext.Set<Application>().AddRangeAsync(app1, app2);
		await _dbContext.SaveChangesAsync();

		var nonExistingId = 9999;
		var spec = new ApplicationByIdSpec(nonExistingId);

		var result = await _applicationRepository.FirstOrDefaultAsync(spec, CancellationToken.None);

		result.ShouldBeNull();
	}

	[Fact]
	public async Task Spec_ShouldReturnNull_WhenDatabaseIsEmpty()
	{
		var spec = new ApplicationByIdSpec(1);

		var result = await _applicationRepository.FirstOrDefaultAsync(spec, CancellationToken.None);

		result.ShouldBeNull();
	}

	private static Application CreateTestApplication(string debugName, int? userId = null)
	{
		var personal = new ApplicantPersonalInfo("Jan", "Kowalski", 35);
		var contact = new ApplicantContactInfo(
			"jan.test@example.com",
			"+48123456789",
			"Testowa 5, Warszawa"
		);
		var financials = new ApplicantFinancialInfo(
			Income: 6200m,
			Costs: 2800m,
			Dependents: 1,
			Job: "Tester"
		);
		var conditions = new OfferConditions(
			Amount: 18000m,
			Duration: 24,
			InterestRate: 8.99m
		);

		var app = new Application(
			100 + (userId ?? 0),
			userId,
			financials,
			contact,
			personal,
			conditions
		);
		return app;
	}
}