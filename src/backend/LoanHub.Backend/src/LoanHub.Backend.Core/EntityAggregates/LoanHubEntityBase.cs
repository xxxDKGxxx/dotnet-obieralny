namespace LoanHub.Backend.Core.EntityAggregates;

public class LoanHubEntityBase : EntityBase
{
	public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
	public DateTime? DeletedAt { get; private set; } = null;
	public bool IsDeleted { get; private set; } = false;

	public void Delete()
	{
		IsDeleted = true;
		DeletedAt = DateTime.UtcNow;
	}
}