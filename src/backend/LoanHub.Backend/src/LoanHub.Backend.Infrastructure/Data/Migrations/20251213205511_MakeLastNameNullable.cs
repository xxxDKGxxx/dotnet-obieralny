using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoanHub.Backend.Infrastructure.Data.Migrations;

/// <inheritdoc />
public partial class MakeLastNameNullable : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.AlterColumn<string>(
			name: "LastName",
			table: "Users",
			type: "nvarchar(50)",
			maxLength: 50,
			nullable: true,
			oldClrType: typeof(string),
			oldType: "nvarchar(50)",
			oldMaxLength: 50);
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.AlterColumn<string>(
			name: "LastName",
			table: "Users",
			type: "nvarchar(50)",
			maxLength: 50,
			nullable: false,
			defaultValue: "",
			oldClrType: typeof(string),
			oldType: "nvarchar(50)",
			oldMaxLength: 50,
			oldNullable: true);
	}
}