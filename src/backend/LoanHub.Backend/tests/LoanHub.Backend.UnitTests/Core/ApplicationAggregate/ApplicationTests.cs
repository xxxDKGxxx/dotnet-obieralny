//using LoanHub.Backend.Core.EntityAggregates.ApplicationAggregate;

//namespace LoanHub.Backend.UnitTests.Core.ApplicationAggregate;

//public class ApplicationTests
//{
//	[Fact]
//	public void Constructor_ShouldInitializeAggregate_Correctly()
//	{
//		var title = "Super Pożyczka";
//		var description = "Najlepsza oferta na rynku - 30 grudnia";
//		var offerId = 2;
//		var userId = 15;
//		var amount = 150000;
//		var duration = 48u;
//		var interestRate = 5.2M;
//		var email = "abc@xyz.com";
//		var firstName = "John";
//		var lastName = "Wilk";
//		var address = "Warszawa Riviera";
//		var job = "Cocaine dealer";
//		var phone = "694209696";
//		var costs = 4000.0M;
//		var income = 6000.0M;
//		var age = 30;
//		var dependents = 5;

//		var application = new Application(
//			title,
//			description,
//			offerId,
//			amount,
//			duration,
//			interestRate,
//			email,
//			firstName,
//			lastName,
//			address,
//			job,
//			phone,
//			costs,
//			income,
//			age,
//			dependents,
//			userId
//		);

//		application.ShouldSatisfyAllConditions(
//			() =>
//			{
//				application.Title.ShouldBe(title);
//			},
//			() =>
//			{
//				application.Description.ShouldBe(description);
//			},
//			() =>
//			{
//				application.OfferId.ShouldBe(offerId);
//			},
//			() =>
//			{
//				application.UserId.ShouldBe(userId);
//			},
//			() =>
//			{
//				application.BankEmployeeId.ShouldBe(null);
//			},
//			() =>
//			{
//				application.Status.ShouldBe(ApplicationStatus.Created);
//			},
//			() =>
//			{
//				application.Amount.ShouldBe(amount);
//			},
//			() =>
//			{
//				application.CreatedAt.ShouldBeInRange(DateTime.UtcNow.AddSeconds(-1), DateTime.UtcNow.AddSeconds(1));
//			},
//			() =>
//			{
//				application.Duration.ShouldBe(duration);
//			},
//			() =>
//			{
//				application.UpdatedAt.ShouldBe(application.CreatedAt);
//			},
//			() =>
//			{
//				application.InterestRate.ShouldBe(interestRate);
//			},
//			() =>
//			{
//				application.Email.ShouldBe(email);
//			},
//			() =>
//			{
//				application.FirstName.ShouldBe(firstName);
//			},
//			() =>
//			{
//				application.LastName.ShouldBe(lastName);
//			},
//			() =>
//			{
//				application.Address.ShouldBe(address);
//			},
//			() =>
//			{
//				application.Phone.ShouldBe(phone);
//			},
//			() =>
//			{
//				application.Job.ShouldBe(job);
//			},
//			() =>
//			{
//				application.Income.ShouldBe(income);
//			},
//			() =>
//			{
//				application.Costs.ShouldBe(costs);
//			},
//			() =>
//			{
//				application.Age.ShouldBe(age);
//			},
//			() =>
//			{
//				application.Dependents.ShouldBe(dependents);
//			});
//	}
//}