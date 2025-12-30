namespace LoanHub.Backend.Core.EntityAggregates.ApplicationAggregate;

public sealed class Application :
	LoanHubEntityBase,
	IAggregateRoot
{
	public string Title { get; private set; }
	public string Description { get; private set; }
	public int OfferId { get; private set; }
	public int UserId { get; private set; }
	public int? BankEmployeeId { get; private set; }
	public ApplicationStatus Status { get; private set; }
	public decimal Amount { get; private set; }
	public uint Duration { get; private set; }
	public decimal InterestRate { get; private set; }
	public DateTime UpdatedAt { get; private set; }

	public Application(string title, string description, int offerId, int userId, decimal amount, uint duration, decimal interestRate, ApplicationStatus? status = null)
	{
		Title = title;
		Description = description;
		OfferId = offerId;
		UserId = userId;
		Status = status ?? ApplicationStatus.Created;
		Amount = amount;
		Duration = duration;
		InterestRate = interestRate;
		UpdatedAt = CreatedAt;
	}

	private void SetStatus(ApplicationStatus status)
	{
		Status = status;
		UpdatedAt = DateTime.Now;
	}

	private void SetBankEmployeeId(int? bankEmployeeId)
	{
		BankEmployeeId = bankEmployeeId;
	}
}