using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoanHub.Backend.Infrastructure.Data.Migrations;

/// <inheritdoc />
public partial class ApplicationAggregate : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.CreateTable(
			name: "Applications",
			columns: table =>
			{
				return new
				{
					Id = table.Column<int>(type: "int", nullable: false)
										.Annotation("SqlServer:Identity", "1, 1"),
					Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
					Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
					OfferId = table.Column<int>(type: "int", nullable: false),
					UserId = table.Column<int>(type: "int", nullable: false),
					BankEmployeeId = table.Column<int>(type: "int", nullable: true),
					Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
					Duration = table.Column<long>(type: "bigint", nullable: false),
					InterestRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
					UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
					CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
					DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
					IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
				};
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_Applications", x => x.Id);
				table.ForeignKey(
					name: "FK_Applications_Offers_OfferId",
					column: x => x.OfferId,
					principalTable: "Offers",
					principalColumn: "Id",
					onDelete: ReferentialAction.Restrict);
				table.ForeignKey(
					name: "FK_Applications_Users_BankEmployeeId",
					column: x => x.BankEmployeeId,
					principalTable: "Users",
					principalColumn: "Id",
					onDelete: ReferentialAction.SetNull);
				table.ForeignKey(
					name: "FK_Applications_Users_UserId",
					column: x => x.UserId,
					principalTable: "Users",
					principalColumn: "Id",
					onDelete: ReferentialAction.Restrict);
			});

		migrationBuilder.CreateIndex(
			name: "IX_Applications_BankEmployeeId",
			table: "Applications",
			column: "BankEmployeeId");

		migrationBuilder.CreateIndex(
			name: "IX_Applications_OfferId",
			table: "Applications",
			column: "OfferId");

		migrationBuilder.CreateIndex(
			name: "IX_Applications_UserId",
			table: "Applications",
			column: "UserId");
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropTable(
			name: "Applications");
	}
}