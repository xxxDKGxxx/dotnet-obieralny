namespace LoanHub.Aggregator.Web.Endpoints.Applications.Post;

public sealed record PostApplicationRequest(
	int OfferId,
	int? UserId,
	decimal Amount,
	uint Duration,
	ApplicantFinancialInfo Financials,
	ApplicantContactInfo Contact,
	ApplicantPersonalInfo PersonalData,
	string ProviderType);