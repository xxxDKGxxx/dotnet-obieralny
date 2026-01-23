namespace LoanHub.Backend.Core.EntityAggregates.ApplicationAggregate;

public sealed class Application :
	LoanHubEntityBase,
	IAggregateRoot
{
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
		Status = ApplicationStatus.Submitted;
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