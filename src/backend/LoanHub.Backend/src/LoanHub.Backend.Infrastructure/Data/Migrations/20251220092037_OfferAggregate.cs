using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoanHub.Backend.Infrastructure.Data.Migrations;

/// <inheritdoc />
public partial class OfferAggregate : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.AlterColumn<bool>(
			name: "IsDeleted",
			table: "Users",
			type: "bit",
			nullable: false,
			defaultValue: false,
			oldClrType: typeof(bool),
			oldType: "bit");

		migrationBuilder.CreateTable(
			name: "Offers",
			columns: table =>
			{
				return new
				{
					Id = table.Column<int>(type: "int", nullable: false)
									.Annotation("SqlServer:Identity", "1, 1"),
					Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
					Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
					AmountRange_Min = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
					AmountRange_Max = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
					DurationRange_Min = table.Column<long>(type: "bigint", nullable: false),
					DurationRange_Max = table.Column<long>(type: "bigint", nullable: false),
					InterestRateRange_Min = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
					InterestRateRange_Max = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
					ValidRange_Min = table.Column<DateTime>(type: "datetime2", nullable: false),
					ValidRange_Max = table.Column<DateTime>(type: "datetime2", nullable: false),
					CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
					DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
					IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
				};
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_Offers", x => x.Id);
			});
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropTable(
			name: "Offers");

		migrationBuilder.AlterColumn<bool>(
			name: "IsDeleted",
			table: "Users",
			type: "bit",
			nullable: false,
			oldClrType: typeof(bool),
			oldType: "bit",
			oldDefaultValue: false);
	}
}