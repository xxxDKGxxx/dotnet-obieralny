using LoanHub.Backend.Core.EntityAggregates.UserAggregate;

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
	public string? LastStatusChangeMessage { get; private set; } = null;

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

	public void SetStatus(ApplicationStatus status, UserRole byWho)
	{
		if (!Status.CanTransitionTo(status, byWho))
		{
			throw new InvalidOperationException($"Cannot transition from {Status.Value} to {status.Value}.");
		}

		Status = status;
	}

	public void SetStatusChangeMessage(string? message)
	{
		LastStatusChangeMessage = message;
	}
}

public sealed record ApplicantContactInfo(string Email, string PhoneNumber, string Address);
public sealed record ApplicantFinancialInfo(decimal Income, decimal Costs, int Dependents, string Job);
public sealed record ApplicantPersonalInfo(string FirstName, string LastName, int Age);
public sealed record OfferConditions(decimal Amount, uint Duration, decimal InterestRate);