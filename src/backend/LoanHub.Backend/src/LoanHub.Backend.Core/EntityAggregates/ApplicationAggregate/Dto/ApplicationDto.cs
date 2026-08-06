namespace LoanHub.Backend.Core.EntityAggregates.ApplicationAggregate.Dto;

public sealed record ApplicationDto(
	int Id,
	int OfferId,
	int? UserId,
	string Status,
	ApplicantContactInfo ContactInfo,
	ApplicantFinancialInfo ApplicantFinancials,
	ApplicantPersonalInfo PersonalData,
	OfferConditions OfferConditions,
	string? DocumentId,
	string? LastStatusChangeMessage);