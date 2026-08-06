using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoanHub.Aggregator.Infrastructure.Data.Migrations;

/// <inheritdoc />
public partial class Counter : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.CreateTable(
			name: "Counters",
			columns: table =>
			{
				return new
				{
					Id = table.Column<int>(type: "int", nullable: false),
					Value = table.Column<int>(type: "int", nullable: false)
				};
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_Counters", x => x.Id);
			});

		migrationBuilder.InsertData(
			table: "Counters",
			columns: ["Id", "Value"],
			values: [1, 0]);
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropTable(
			name: "Counters");
	}
}