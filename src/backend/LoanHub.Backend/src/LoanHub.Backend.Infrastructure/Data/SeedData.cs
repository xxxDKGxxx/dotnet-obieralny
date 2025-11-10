namespace LoanHub.Backend.Infrastructure.Data;

public static class SeedData
{
	public static async Task InitializeAsync(AppDbContext dbContext)
	{
		// here check if the data is already in database

		await PopulateTestDataAsync(dbContext);
	}

	public static async Task PopulateTestDataAsync(AppDbContext dbContext)
	{
		// here add data to database

		_ = await dbContext.SaveChangesAsync();
	}
}