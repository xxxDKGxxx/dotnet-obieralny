using FastEndpoints;
using LoanHub.Backend.Core.EntityAggregates.UserAggregate.Specifications;
using Microsoft.AspNetCore.Http;

namespace LoanHub.Backend.Infrastructure.Data;

public static class SeedData
{
	public static async Task InitializeAsync(AppDbContext dbContext)
	{
		// here check if the data is already in database

		if (!dbContext.Set<Offer>().
			Any())
		{
			await SeedOffers(dbContext);
		}
	}

	public static async Task InitializeTestAsync(AppDbContext dbContext)
	{
		var user = new User("test.user@example.com", "Test", "Mock", UserRole.User);
		var employee = new User("test.employee@example.com", "Test", "Employee", UserRole.Employee);

		if (!dbContext.Set<User>().Any(u => u.Email == user.Email))
		{
			await SeedTestUser(dbContext, user);
		}

		if (!dbContext.Set<User>().Any(u => u.Email == employee.Email))
		{
			await SeedTestUser(dbContext, employee);
		}

	}

	private static async Task SeedTestUser(AppDbContext dbContext, User user)
	{
		await dbContext.AddAsync(user);
		_ = await dbContext.SaveChangesAsync();
	}

	private static async Task SeedOffers(AppDbContext dbContext)
	{
		var offers = new List<Offer>
		{
			new(
				"Szybka Gotówka Mini",
				"Mała pożyczka na nagłe wydatki do spłaty w kilka miesięcy.",
				new AmountRange(500m, 5000m),
				new DurationRange(3, 12),
				new InterestRateRange(10m, 18m),
				new ValidRange(DateTime.UtcNow.AddDays(-10), DateTime.UtcNow.AddYears(1))
			),
			new(
				"Wakacyjny Luz",
				"Sfinansuj swoje wymarzone wakacje z odroczoną płatnością.",
				new AmountRange(2000m, 15000m),
				new DurationRange(6, 24),
				new InterestRateRange(8.5m, 14m),
				new ValidRange(DateTime.UtcNow.AddDays(-5), DateTime.UtcNow.AddMonths(6))
			),
			new(
				"Remont Express",
				"Gotówka na odświeżenie mieszkania bez zbędnych formalności.",
				new AmountRange(5000m, 50000m),
				new DurationRange(12, 60),
				new InterestRateRange(7.5m, 12m),
				new ValidRange(DateTime.UtcNow.AddMonths(-1), DateTime.UtcNow.AddYears(2))
			),
			new(
				"Auto Marzeń",
				"Niskooprocentowany kredyt na zakup samochodu nowego lub używanego.",
				new AmountRange(10000m, 150000m),
				new DurationRange(24, 96),
				new InterestRateRange(6m, 10m),
				new ValidRange(DateTime.UtcNow.AddMonths(-2), DateTime.UtcNow.AddYears(3))
			),
			new(
				"Studencki Start",
				"Elastyczna pożyczka dla studentów z preferencyjnymi warunkami.",
				new AmountRange(1000m, 10000m),
				new DurationRange(12, 48),
				new InterestRateRange(5m, 9m),
				new ValidRange(DateTime.UtcNow.AddDays(-20), DateTime.UtcNow.AddYears(1))
			),
			new(
				"Konsolidacja Premium",
				"Połącz swoje zobowiązania w jedną niższą ratę.",
				new AmountRange(20000m, 200000m),
				new DurationRange(36, 120),
				new InterestRateRange(6.5m, 9.5m),
				new ValidRange(DateTime.UtcNow.AddMonths(-6), DateTime.UtcNow.AddYears(5))
			),
			new(
				"Eko Dom",
				"Dofinansowanie na panele fotowoltaiczne i pompy ciepła.",
				new AmountRange(15000m, 80000m),
				new DurationRange(24, 84),
				new InterestRateRange(4.5m, 8m),
				new ValidRange(DateTime.UtcNow.AddDays(-1), DateTime.UtcNow.AddYears(2))
			),
			new(
				"Technologia dla Ciebie",
				"Kredyt na laptopa, telefon lub sprzęt RTV.",
				new AmountRange(1000m, 20000m),
				new DurationRange(6, 36),
				new InterestRateRange(9m, 16m),
				new ValidRange(DateTime.UtcNow, DateTime.UtcNow.AddMonths(12))
			),
			new(
				"Złota Jesień",
				"Specjalna oferta dla seniorów na dowolny cel.",
				new AmountRange(1000m, 15000m),
				new DurationRange(12, 36),
				new InterestRateRange(8m, 11m),
				new ValidRange(DateTime.UtcNow.AddMonths(-3), DateTime.UtcNow.AddYears(1))
			),
			new(
				"Pierwszy Biznes",
				"Wsparcie na start dla nowych przedsiębiorców.",
				new AmountRange(10000m, 100000m),
				new DurationRange(12, 60),
				new InterestRateRange(9m, 15m),
				new ValidRange(DateTime.UtcNow.AddDays(-15), DateTime.UtcNow.AddYears(2))
			),
			new(
				"Rowerowy Zawrót Głowy",
				"Kup wymarzony jednoślad na raty 0%.",
				new AmountRange(2000m, 25000m),
				new DurationRange(10, 20),
				new InterestRateRange(0.1m, 5m),
				new ValidRange(DateTime.UtcNow.AddDays(-10), DateTime.UtcNow.AddDays(60))
			),
			new(
				"Medyczny Spokój",
				"Środki na zabiegi medyczne, stomatologię i rehabilitację.",
				new AmountRange(3000m, 40000m),
				new DurationRange(12, 48),
				new InterestRateRange(7m, 11.5m),
				new ValidRange(DateTime.UtcNow.AddMonths(-1), DateTime.UtcNow.AddYears(1))
			),
			new(
				"Ślubne Marzenia",
				"Gotówka na organizację wesela i podróż poślubną.",
				new AmountRange(10000m, 60000m),
				new DurationRange(24, 60),
				new InterestRateRange(8m, 13m),
				new ValidRange(DateTime.UtcNow.AddMonths(-2), DateTime.UtcNow.AddYears(1))
			),
			new(
				"Bezpieczna Przystań",
				"Długoterminowa pożyczka hipoteczna na dowolny cel.",
				new AmountRange(50000m, 300000m),
				new DurationRange(60, 240),
				new InterestRateRange(5.5m, 8.5m),
				new ValidRange(DateTime.UtcNow.AddMonths(-12), DateTime.UtcNow.AddYears(5))
			),
			new(
				"VIP Prestige",
				"Oferta limitowana dla stałych klientów o wysokich dochodach.",
				new AmountRange(50000m, 500000m),
				new DurationRange(12, 60),
				new InterestRateRange(4m, 7m),
				new ValidRange(DateTime.UtcNow.AddDays(-5), DateTime.UtcNow.AddDays(10))
			),
			new(
				"Edukacja Przyszłości",
				"Sfinansuj studia podyplomowe lub kursy językowe.",
				new AmountRange(2000m, 25000m),
				new DurationRange(12, 36),
				new InterestRateRange(6m, 10m),
				new ValidRange(DateTime.UtcNow.AddDays(-20), DateTime.UtcNow.AddYears(1))
			),
			new(
				"Zimowe Szaleństwo",
				"Szybka gotówka na ferie zimowe i sprzęt narciarski.",
				new AmountRange(1000m, 10000m),
				new DurationRange(3, 12),
				new InterestRateRange(11m, 17m),
				new ValidRange(DateTime.UtcNow.AddMonths(-1), DateTime.UtcNow.AddMonths(3))
			),
			new(
				"Gamer Zone",
				"Kredyt na stanowisko gamingowe i konsole nowej generacji.",
				new AmountRange(3000m, 15000m),
				new DurationRange(6, 24),
				new InterestRateRange(12m, 18m),
				new ValidRange(DateTime.UtcNow, DateTime.UtcNow.AddMonths(6))
			),
			new(
				"Ogród Marzeń",
				"Środki na aranżację ogrodu, altany i systemy nawadniania.",
				new AmountRange(5000m, 30000m),
				new DurationRange(12, 48),
				new InterestRateRange(7m, 12m),
				new ValidRange(DateTime.UtcNow.AddMonths(-2), DateTime.UtcNow.AddYears(1))
			),
			new(
				"Naprawa Samochodu",
				"Nieplanowana wizyta u mechanika? Pomożemy.",
				new AmountRange(500m, 8000m),
				new DurationRange(3, 18),
				new InterestRateRange(13m, 19m),
				new ValidRange(DateTime.UtcNow.AddMonths(-6), DateTime.UtcNow.AddYears(2))
			)
		};

		await dbContext.Set<Offer>().AddRangeAsync(offers);
		_ = await dbContext.SaveChangesAsync();
	}
}