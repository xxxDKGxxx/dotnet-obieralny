namespace LoanHub.Backend.Core.MyEntityAggregate;

public interface IsAudible
{
	DateTime CreatedAt {get; set;}
	DateTime? UpdatedAt {get; set;}
}

public interface IsSoftDeletable
{
	bool IsDeleted {get; set;}
	DateTime? DeletedAt {get; set;}
}



public class BaseEntity: IsAudible, IsSoftDeletable
{
	public DateTime CreatedAt{get;set;}

	public DateTime? UpdatedAt { get; set; }

	public bool IsDeleted { get; set; }

	public DateTime? DeletedAt { get; set; }
}