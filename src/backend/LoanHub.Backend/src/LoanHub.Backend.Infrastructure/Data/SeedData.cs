<<<<<<< HEAD
// using LoanHub.Backend.Core.[EntityAggregate];

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

        await dbContext.SaveChangesAsync();
    }
}
=======
using LoanHub.Backend.Core.ContributorAggregate;

namespace LoanHub.Backend.Infrastructure.Data;

public static class SeedData
{
    public static readonly Contributor Contributor1 = new("Ardalis");
    public static readonly Contributor Contributor2 = new("Snowfrog");

    public static async Task InitializeAsync(AppDbContext dbContext)
    {
        if (await dbContext.Contributors.AnyAsync())
            return; // DB has been seeded

        await PopulateTestDataAsync(dbContext);
    }

    public static async Task PopulateTestDataAsync(AppDbContext dbContext)
    {
        dbContext.Contributors.AddRange([Contributor1, Contributor2]);
        await dbContext.SaveChangesAsync();
    }
}
>>>>>>> f2068df54d58808223d1f0cbbc672e2323529db6
