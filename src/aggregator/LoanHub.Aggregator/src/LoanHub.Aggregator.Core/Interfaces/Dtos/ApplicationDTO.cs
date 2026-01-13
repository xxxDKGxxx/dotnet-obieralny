namespace LoanHub.Aggregator.Core.Interfaces.Dtos;
public sealed record ApplicationDTO(
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
	int Dependents
);
