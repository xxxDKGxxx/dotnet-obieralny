namespace LoanHub.Backend.UnitTests.Core.ApplicationAggregate;

public class ApplicationTests
{
	[Fact]
	public void Constructor_ShouldInitializeAggregate_Correctly()
	{
		var offerId = 42;
		var userId = 17;
		var personalData = new ApplicantPersonalInfo("Jan", "Kowalski", 35);
		var contactInfo = new ApplicantContactInfo(
			"jan.kowalski@example.com",
			"+48 123 456 789",
			"ul. Testowa 12, 00-000 Warszawa"
		);
		var financials = new ApplicantFinancialInfo(
			Income: 6500m,
			Costs: 3200m,
			Dependents: 2,
			Job: "Software Developer"
		);
		var conditions = new OfferConditions(
			Amount: 25000m,
			Duration: 18,
			InterestRate: 9.99m
		);

		var application = new Application(
			offerId,
			userId,
			financials,
			contactInfo,
			personalData,
			conditions
		);

		application.ShouldSatisfyAllConditions(
			() => application.OfferId.ShouldBe(offerId),
			() => application.UserId.ShouldBe(userId),
			() => application.Status.ShouldBe(ApplicationStatus.Created),
			() => application.DocumentId.ShouldBeNull(),
			// PersonalData
			() => application.PersonalData.ShouldBe(personalData),
			() => application.PersonalData.FirstName.ShouldBe("Jan"),
			() => application.PersonalData.LastName.ShouldBe("Kowalski"),
			() => application.PersonalData.Age.ShouldBe(35),
			// ContactInfo
			() => application.ContactInfo.ShouldBe(contactInfo),
			() => application.ContactInfo.Email.ShouldBe("jan.kowalski@example.com"),
			() => application.ContactInfo.PhoneNumber.ShouldBe("+48 123 456 789"),
			() => application.ContactInfo.Address.ShouldBe("ul. Testowa 12, 00-000 Warszawa"),
			// ApplicantFinancials
			() => application.ApplicantFinancials.ShouldBe(financials),
			() => application.ApplicantFinancials.Income.ShouldBe(6500m),
			() => application.ApplicantFinancials.Costs.ShouldBe(3200m),
			() => application.ApplicantFinancials.Dependents.ShouldBe(2),
			() => application.ApplicantFinancials.Job.ShouldBe("Software Developer"),
			// OfferConditions
			() => application.OfferConditions.ShouldBe(conditions),
			() => application.OfferConditions.Amount.ShouldBe(25000m),
			() => application.OfferConditions.Duration.ShouldBe((uint)18),
			() => application.OfferConditions.InterestRate.ShouldBe(9.99m),
			// Base / audit fields
			() => application.Id.ShouldBe(0),
			() => application.CreatedAt.ShouldBeInRange(
				DateTime.UtcNow.AddSeconds(-2),
				DateTime.UtcNow.AddSeconds(2)
			),
			() => application.IsDeleted.ShouldBeFalse()
		);
	}

	[Fact]
	public void Constructor_WithNullUserId_ShouldStillInitializeCorrectly()
	{
		var offerId = 7;
		ApplicantPersonalInfo personal = new("Anna", "Nowak", 28);
		ApplicantContactInfo contact = new("anna.nowak@test.pl", "600700800", "Kraków");
		ApplicantFinancialInfo fin = new(4200m, 1800m, 0, "Freelancer");
		OfferConditions cond = new(8000m, 12, 14.5m);

		var application = new Application(
			offerId,
			userId: null,
			financials: fin,
			contact: contact,
			personalData: personal,
			conditions: cond
		);

		application.UserId.ShouldBeNull();
		application.Status.ShouldBe(ApplicationStatus.Created);
		application.OfferId.ShouldBe(offerId);
		application.IsDeleted.ShouldBeFalse();
		application.CreatedAt.ShouldBeInRange(DateTime.UtcNow.AddSeconds(-2), DateTime.UtcNow.AddSeconds(2));
	}
}