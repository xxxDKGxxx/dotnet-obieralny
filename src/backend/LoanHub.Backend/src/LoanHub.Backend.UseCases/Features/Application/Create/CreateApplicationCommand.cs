namespace LoanHub.Backend.UseCases.Features.Application.Create;

public sealed record CreateApplicationCommand(
	int OfferId,
	int? UserId,
	int? RequestingUserId,
	decimal Amount,
	uint Duration,
	ApplicantFinancialInfo Financials,
	ApplicantContactInfo Contact,
	ApplicantPersonalInfo PersonalData) : ICommand<Result<ApplicationDto>>;