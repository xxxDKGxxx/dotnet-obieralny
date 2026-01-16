namespace LoanHub.Backend.Core.EntityAggregates.ApplicationAggregate;

public sealed class Application :
	LoanHubEntityBase,
	IAggregateRoot
{
<<<<<<< HEAD
	public int OfferId { get; private set; }
	public int? UserId { get; private set; }
	public ApplicationStatus Status { get; private set; }
	public ApplicantContactInfo ContactInfo { get; private set; }
	public ApplicantFinancialInfo ApplicantFinancials { get; private set; }
	public ApplicantPersonalInfo PersonalData { get; private set; }
	public OfferConditions OfferConditions { get; private set; }
	public string? DocumentId { get; private set; } = null;

	public Application(
		int offerId,
		int? userId,
		ApplicantFinancialInfo financials,
		ApplicantContactInfo contact,
		ApplicantPersonalInfo personalData,
		OfferConditions conditions)
	{
		OfferId = offerId;
		UserId = userId;
		ContactInfo = contact;
		ApplicantFinancials = financials;
		PersonalData = personalData;
		OfferConditions = conditions;
		Status = ApplicationStatus.Created;
	}

#pragma warning disable CS8618
	private Application() { /* EF */ }
#pragma warning restore CS8618

	private void SetStatus(ApplicationStatus status)
	{
		Status = status;
	}

}

public sealed record ApplicantContactInfo(string Email, string PhoneNumber, string Address);
public sealed record ApplicantFinancialInfo(decimal Income, decimal Costs, int Dependents, string Job);
public sealed record ApplicantPersonalInfo(string FirstName, string LastName, int Age);
public sealed record OfferConditions(decimal Amount, uint Duration, decimal InterestRate);
=======
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
>>>>>>> 7ad7476eeea0e2f4c266a6307f360e22ed70bb07
