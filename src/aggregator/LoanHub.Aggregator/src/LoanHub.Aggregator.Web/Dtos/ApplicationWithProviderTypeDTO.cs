using LoanHub.Aggregator.Core.ApplicationAggregate;

namespace LoanHub.Aggregator.Web.Dtos;

public sealed record ApplicationWithProviderTypeDTO(
	int Id,
	string Title,
	string Description,
	string Status,
	decimal Amount,
	uint Duration,
	decimal InterestRate,
	DateTime UpdatedAt,
	string Email,
	string FirstName,
	string LastName,
	string Address,
	string Phone,
	string Job,
	decimal Income,
	decimal Costs,
	int Age,
	int Dependents,
	string ProviderType
);
