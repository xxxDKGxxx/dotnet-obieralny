namespace LoanHub.Backend.Web.Endpoints.Application.Post;

public sealed record PostApplicationRequest(
	int OfferId,
	int? UserId,
	decimal Amount,
	uint Duration,
	ApplicantFinancialInfo Financials,
	ApplicantContactInfo Contact,
	ApplicantPersonalInfo PersonalData);