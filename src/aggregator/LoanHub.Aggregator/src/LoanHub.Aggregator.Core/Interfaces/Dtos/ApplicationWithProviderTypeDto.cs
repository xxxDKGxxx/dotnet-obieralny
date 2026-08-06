namespace LoanHub.Aggregator.Core.Interfaces.Dtos;

public record ApplicationWithProviderTypeDto(
	int Id,
	int OfferId,
	int? UserId,
	string Status,
	ApplicantContactInfo ContactInfo,
	ApplicantFinancialInfo ApplicantFinancials,
	ApplicantPersonalInfo PersonalData,
	OfferConditions OfferConditions,
	string? DocumentId,
	string? LastStatusChangeMessage,
	string ProviderType);

public sealed record ApplicantContactInfo(string Email, string PhoneNumber, string Address);
public sealed record ApplicantFinancialInfo(decimal Income, decimal Costs, int Dependents, string Job);
public sealed record ApplicantPersonalInfo(string FirstName, string LastName, int Age);
public sealed record OfferConditions(decimal Amount, uint Duration, decimal InterestRate);