namespace LoanHub.Backend.UnitTests.Core;

public class LoanHubBaseTests
{
	[Fact]
	public void LoanHubBase_WhenCreated_ShouldHaveCorrectCreatedAtDeletedAtAndIsDeletedSet()
	{

		var beforeCreationTime = DateTime.UtcNow;
		var entity = new LoanHubEntityBase();
		var afterCreationTime = DateTime.UtcNow;

		entity.CreatedAt
			.ShouldBeInRange(beforeCreationTime, afterCreationTime);

		entity.DeletedAt
			.ShouldBeNull();

		entity.IsDeleted
			.ShouldBeFalse();
	}

	[Fact]
	public void LoanHubBase_WhenDeleted_ShouldHaveCorrectDeletedAtIsDeletedSet()
	{
		var entity = new LoanHubEntityBase();

		var beforeDeletionTime = DateTime.UtcNow;

		entity.Delete();

		var afterDeletionTime = DateTime.UtcNow;

		entity.DeletedAt
			.ShouldNotBeNull()
			.ShouldBeInRange(beforeDeletionTime, afterDeletionTime);

		entity.IsDeleted
			.ShouldBeTrue();
	}
}