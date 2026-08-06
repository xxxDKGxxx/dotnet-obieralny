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
					OfferId = table.Column<int>(type: "int", nullable: false),
					UserId = table.Column<int>(type: "int", nullable: true),
					Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
					ContactInfo_Email = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: false),
					ContactInfo_PhoneNumber = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false),
					ContactInfo_Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
					ApplicantFinancials_Income = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
					ApplicantFinancials_Costs = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
					ApplicantFinancials_Dependents = table.Column<int>(type: "int", nullable: false),
					ApplicantFinancials_Job = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
					PersonalData_FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
					PersonalData_LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
					PersonalData_Age = table.Column<int>(type: "int", nullable: false),
					OfferConditions_Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
					OfferConditions_Duration = table.Column<long>(type: "bigint", nullable: false),
					OfferConditions_InterestRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
					DocumentId = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
					name: "FK_Applications_Users_UserId",
					column: x => x.UserId,
					principalTable: "Users",
					principalColumn: "Id",
					onDelete: ReferentialAction.Restrict);
			});

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