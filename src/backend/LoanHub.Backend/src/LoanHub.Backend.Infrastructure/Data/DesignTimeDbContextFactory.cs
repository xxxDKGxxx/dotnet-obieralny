using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace LoanHub.Backend.Infrastructure.Data;

public sealed class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
	public AppDbContext CreateDbContext(string[] args)
	{
		var conn = Environment.GetEnvironmentVariable("LOANHUB_CONNECTION_STRING");
		if (string.IsNullOrWhiteSpace(conn))
			throw new InvalidOperationException(
				"LOANHUB_CONNECTION_STRING environment variable is required but not found. Please set it to a valid SQL Server connection string.");

		var options = new DbContextOptionsBuilder<AppDbContext>()
			.UseSqlServer(conn, sql => sql.MigrationsAssembly(typeof(DesignTimeDbContextFactory).Assembly.FullName))
			.Options;

		return new AppDbContext(options, dispatcher: null);
	}
}