using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace LoanHub.Backend.Infrastructure.Data;

public sealed class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
	public AppDbContext CreateDbContext(string[] args)
	{
		var conn = Environment.GetEnvironmentVariable("DefaultConnection");
		if (string.IsNullOrWhiteSpace(conn))
			throw new InvalidOperationException(
				"DefaultConnection environment variable is required but not found. Please set it to a valid SQL Server connection string.");

		var options = new DbContextOptionsBuilder<AppDbContext>()
			.UseSqlServer(conn, sql => sql.MigrationsAssembly(typeof(DesignTimeDbContextFactory).Assembly.FullName))
			.Options;

		return new AppDbContext(options, dispatcher: null);
	}
}