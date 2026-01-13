using LoanHub.Backend.Core.EntityAggregates.OfferAggregate;
using LoanHub.Backend.Core.EntityAggregates.UserAggregate;

namespace LoanHub.Backend.Core.EntityAggregates.ApplicationAggregate;

public sealed class Application :
	LoanHubEntityBase,
	IAggregateRoot
{
	public int OfferId { get; private set; }
	public Offer Offer { get; private set; }
	public int? UserId { get; private set; }
	public User? User { get; private set; }
	public ApplicationStatus Status { get; private set; }
	public decimal Amount { get; private set; }
	public uint Duration { get; private set; }
	public decimal InterestRate { get; private set; }
	public DateTime UpdatedAt { get; private set; }
	public string Email { get; private set; }
	public string FirstName { get; private set; }
	public string LastName { get; private set; }
	public string Address { get; private set; }
	public string Phone { get; private set; }
	public string Job { get; private set; }
	public decimal Income { get; private set; }
	public decimal Costs { get; private set; }
	public int Age { get; private set; }
	public int Dependents { get; private set; }

#pragma warning disable CS8618
	private Application() { /* EF */ }
#pragma warning restore CS8618

	public Application(
		Offer offer,
		User user,
		decimal amount,
		uint duration,
		decimal interestRate,
		string email,
		string firstName,
		string lastName,
		string address,
		string job,
		string phone,
		decimal costs,
		decimal income,
		int age,
		int dependents)
	{
		OfferId = offer.Id;
		UserId = user?.Id;
		User = user;
		Offer = offer;
		Status = ApplicationStatus.Created;
		Amount = amount;
		Duration = duration;
		InterestRate = interestRate;
		UpdatedAt = CreatedAt;
		Email = email;
		FirstName = firstName;
		LastName = lastName;
		Address = address;
		Phone = phone;
		Job = job;
		Costs = costs;
		Income = income;
		Age = age;
		Dependents = dependents;
	}

	private void SetStatus(ApplicationStatus status)
	{
		Status = status;
		UpdatedAt = DateTime.Now;
	}

}