using LoanHub.Backend.Core.EntityAggregates.ApplicationAggregate;
namespace LoanHub.Backend.UnitTests.Core.ApplicationAggregate;

public class ApplicationTests
{
	[Fact]
	public void Constructor_ShouldInitializeAggregate_Correctly()
	{
		var title = "Super Pożyczka";
		var description = "Najlepsza oferta na rynku - 30 grudnia";
		var offerId = 2;
		var userId = 15;
		var amount = 150000;
		var duration = 48u;
		var interestRate = 5.2M;

		var application = new Application(
			title,
			description,
			offerId,
			userId,
			amount,
			duration,
			interestRate
		);

		application.ShouldSatisfyAllConditions(
			() =>
			{
				application.Title.ShouldBe(title);
			},
			() =>
			{
				application.Description.ShouldBe(description);
			},
			() =>
			{
				application.OfferId.ShouldBe(offerId);
			},
		() =>
			{
				application.UserId.ShouldBe(userId);
			},
			() =>
			{
				application.BankEmployeeId.ShouldBe(null);
			},
			() =>
			{
				application.Status.ShouldBe(ApplicationStatus.Created);
			},
			() =>
			{
				application.Amount.ShouldBe(amount);
			},
			() =>
			{
				application.CreatedAt.ShouldBeInRange(DateTime.UtcNow.AddSeconds(-1), DateTime.UtcNow.AddSeconds(1));
			},
			() =>
			{
				application.Duration.ShouldBe(duration);
			},
			() =>
			{
				application.UpdatedAt.ShouldBe(application.CreatedAt);
			},
			() =>
			{
				application.InterestRate.ShouldBe(interestRate);
			});
	}
}