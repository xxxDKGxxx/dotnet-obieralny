namespace LoanHub.Backend.Core.UserAggregate;

public sealed class User(
	string email,
	string passwordHash,
	string firstName,
	string lastName,
	UserRole role) :
	LoanHubEntityBase,
	IAggregateRoot
{
	public string Email { get; private set; } = email;
	public string PasswordHash { get; private set; } = passwordHash;
	public UserRole Role { get; private set; } = role;
	public string FirstName { get; private set; } = firstName;
	public string LastName { get; private set; } = lastName;

	public string? Address
	{
		get; set;
	}
	public string? Phone
	{
		get; set;
	}
	public decimal? Income
	{
		get; set;
	}
	public decimal? Costs
	{
		get; set;
	}
	public int? Dependents
	{
		get; set;
	}
	public string? Job
	{
		get; set;
	}
	public int? Age
	{
		get; set;
	}

}