using LoanHub.Backend.Core.EntityAggregates.OfferAggregate;

namespace LoanHub.Backend.Core.EntityAggregates.ApplicationAggregate;

public sealed class Application :
	LoanHubEntityBase,
	IAggregateRoot
{
	public string Title { get; private set; }
	public string Description { get; private set; }
	public int OfferId { get; private set; }
	public int? UserId { get; private set; }
	public int? BankEmployeeId { get; private set; }
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

	public Application(
		Offer offer,
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
		int dependents,
		int? userId = null)
	{
		Title = offer.Title;
		Description = offer.Description;
		OfferId = offer.Id;
		UserId = userId;
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

	public Application(
		string title,
		string description,
		int offerId,
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
		int dependents,
		int? userId = null)
	{
		Title = title;
		Description = description;
		OfferId = offerId;
		UserId = userId;
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

	private void SetTitle(string title) { Title = title; }

	private void SetDescription(string description) { Description = description; }

	private void SetStatus(ApplicationStatus status)
	{
		Status = status;
		UpdatedAt = DateTime.Now;
	}

	private void SetBankEmployeeId(int? bankEmployeeId)
	{
		BankEmployeeId = bankEmployeeId;
	}
}