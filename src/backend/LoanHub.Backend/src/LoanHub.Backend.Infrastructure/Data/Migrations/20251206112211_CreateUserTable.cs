using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoanHub.Backend.Infrastructure.Data.Migrations;

/// <inheritdoc />
public partial class CreateUserTable : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.CreateTable(
			name: "Users",
			columns: table =>
			{
				return new
				{
					Id = table.Column<int>(type: "int", nullable: false)
									.Annotation("SqlServer:Identity", "1, 1"),
					Email = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: false),
					Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
					FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
					LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
					Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
					Phone = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: true),
					Job = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
					Income = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
					Costs = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
					Age = table.Column<int>(type: "int", nullable: true),
					Dependents = table.Column<int>(type: "int", nullable: true),
					CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
					DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
					IsDeleted = table.Column<bool>(type: "bit", nullable: false)
				};
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_Users", x => x.Id);
			});

		migrationBuilder.CreateIndex(
			name: "IX_Users_Email",
			table: "Users",
			column: "Email",
			unique: true);
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropTable(
			name: "Users");
	}
}