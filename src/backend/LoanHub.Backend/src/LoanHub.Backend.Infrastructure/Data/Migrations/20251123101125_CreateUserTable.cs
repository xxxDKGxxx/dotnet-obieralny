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
					Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
					PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
					FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
					LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
					Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
					Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
					Income = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
					Costs = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
					Dependents = table.Column<int>(type: "int", nullable: true),
					Job = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
					Age = table.Column<int>(type: "int", nullable: true),
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