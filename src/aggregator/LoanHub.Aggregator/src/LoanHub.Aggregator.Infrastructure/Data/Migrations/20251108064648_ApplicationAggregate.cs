using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoanHub.Aggregator.Infrastructure.Data.Migrations;

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
					UserId = table.Column<int>(type: "int", nullable: false),
					ProviderType = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
					ProviderApplicationId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
					CreatedAt = table.Column<string>(type: "nvarchar(48)", nullable: false),
					DeletedAt = table.Column<string>(type: "nvarchar(48)", nullable: true),
					IsDeleted = table.Column<bool>(type: "bit", nullable: false)
				};
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_Applications", x => x.Id);
			});
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropTable(
			name: "Applications");
	}
}