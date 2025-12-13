using LoanHub.Aggregator.Core.Interfaces;

namespace LoanHub.Aggregator.IntegrationTests.Data;

public class EfRepoTests : BaseEfRepoTestFixture
{
	[Fact]
	public async Task EfRepo_WhenEntityIsDeleted_ShouldCommenceSoftDeleteBehaviour()
	{
		var application = new Application(
			0,
			ApplicationProviderType.ArdalisBank,
			"69");

		var repo = GetRepository();

		var newEntity = await repo.AddAsync(application);

		newEntity.IsDeleted
			.ShouldBeFalse();

		newEntity.DeletedAt
			.ShouldBeNull();

		var beforeDeletionDate = DateTime.UtcNow;
		var deleteResult = await repo.DeleteAsync(application);
		var afterDeletionDate = DateTime.UtcNow;

		deleteResult.ShouldBe(1);

		await repo.SaveChangesAsync();

		newEntity.IsDeleted
			.ShouldBeTrue();

		newEntity.DeletedAt
			.ShouldNotBeNull()
			.ShouldBeInRange(beforeDeletionDate, afterDeletionDate);
	}

	[Fact]
	public async Task EfRepo_WhenEntitiesAreRetrieved_ShouldNotReturnDeletedEntities()
	{
		var app1 = new Application(
			0,
			ApplicationProviderType.ArdalisBank,
			"TestAppId");

		var app2 = new Application(
			0,
			ApplicationProviderType.ArdalisBank,
			"TestAppId");

		var repo = GetRepository();

		var newEntities = (await repo.AddRangeAsync([app1, app2])).ToList();

		await repo.DeleteAsync(newEntities[0]);
		await repo.SaveChangesAsync();

		var currentApplications = await repo.ListAsync();

		newEntities[0].IsDeleted
			.ShouldBeTrue();

		currentApplications.ShouldHaveSingleItem()
			.ShouldBe(newEntities[1]);
	}
}