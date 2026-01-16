namespace LoanHub.Backend.Web.Endpoints.Applications;

public record ApplicationRecord(
	int Id,
	string Title,
	string? Description,
	int? OfferId,
	int UserId,
	int? BankEmployeeId,
	string Status,
	decimal Amount,
	uint Duration,
	decimal InterestRate);