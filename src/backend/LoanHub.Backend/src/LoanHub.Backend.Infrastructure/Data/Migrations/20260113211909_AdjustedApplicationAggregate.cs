using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoanHub.Backend.Infrastructure.Data.Migrations;

/// <inheritdoc />
public partial class AdjustedApplicationAggregate : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropForeignKey(
			name: "FK_Applications_Users_BankEmployeeId",
			table: "Applications");

		migrationBuilder.DropIndex(
			name: "IX_Applications_BankEmployeeId",
			table: "Applications");

		migrationBuilder.DropColumn(
			name: "BankEmployeeId",
			table: "Applications");

		migrationBuilder.DropColumn(
			name: "Description",
			table: "Applications");

		migrationBuilder.RenameColumn(
			name: "Title",
			table: "Applications",
			newName: "Address");

		migrationBuilder.AlterColumn<int>(
			name: "UserId",
			table: "Applications",
			type: "int",
			nullable: true,
			oldClrType: typeof(int),
			oldType: "int");

		migrationBuilder.AddColumn<int>(
			name: "Age",
			table: "Applications",
			type: "int",
			nullable: false,
			defaultValue: 0);

		migrationBuilder.AddColumn<decimal>(
			name: "Costs",
			table: "Applications",
			type: "decimal(18,2)",
			nullable: false,
			defaultValue: 0m);

		migrationBuilder.AddColumn<int>(
			name: "Dependents",
			table: "Applications",
			type: "int",
			nullable: false,
			defaultValue: 0);

		migrationBuilder.AddColumn<string>(
			name: "Email",
			table: "Applications",
			type: "nvarchar(254)",
			maxLength: 254,
			nullable: false,
			defaultValue: "");

		migrationBuilder.AddColumn<string>(
			name: "FirstName",
			table: "Applications",
			type: "nvarchar(50)",
			maxLength: 50,
			nullable: false,
			defaultValue: "");

		migrationBuilder.AddColumn<decimal>(
			name: "Income",
			table: "Applications",
			type: "decimal(18,2)",
			nullable: false,
			defaultValue: 0m);

		migrationBuilder.AddColumn<string>(
			name: "Job",
			table: "Applications",
			type: "nvarchar(100)",
			maxLength: 100,
			nullable: false,
			defaultValue: "");

		migrationBuilder.AddColumn<string>(
			name: "LastName",
			table: "Applications",
			type: "nvarchar(50)",
			maxLength: 50,
			nullable: false,
			defaultValue: "");

		migrationBuilder.AddColumn<string>(
			name: "Phone",
			table: "Applications",
			type: "nvarchar(9)",
			maxLength: 9,
			nullable: false,
			defaultValue: "");
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropColumn(
			name: "Age",
			table: "Applications");

		migrationBuilder.DropColumn(
			name: "Costs",
			table: "Applications");

		migrationBuilder.DropColumn(
			name: "Dependents",
			table: "Applications");

		migrationBuilder.DropColumn(
			name: "Email",
			table: "Applications");

		migrationBuilder.DropColumn(
			name: "FirstName",
			table: "Applications");

		migrationBuilder.DropColumn(
			name: "Income",
			table: "Applications");

		migrationBuilder.DropColumn(
			name: "Job",
			table: "Applications");

		migrationBuilder.DropColumn(
			name: "LastName",
			table: "Applications");

		migrationBuilder.DropColumn(
			name: "Phone",
			table: "Applications");

		migrationBuilder.RenameColumn(
			name: "Address",
			table: "Applications",
			newName: "Title");

		migrationBuilder.AlterColumn<int>(
			name: "UserId",
			table: "Applications",
			type: "int",
			nullable: false,
			defaultValue: 0,
			oldClrType: typeof(int),
			oldType: "int",
			oldNullable: true);

		migrationBuilder.AddColumn<int>(
			name: "BankEmployeeId",
			table: "Applications",
			type: "int",
			nullable: true);

		migrationBuilder.AddColumn<string>(
			name: "Description",
			table: "Applications",
			type: "nvarchar(max)",
			nullable: false,
			defaultValue: "");

		migrationBuilder.CreateIndex(
			name: "IX_Applications_BankEmployeeId",
			table: "Applications",
			column: "BankEmployeeId");

		migrationBuilder.AddForeignKey(
			name: "FK_Applications_Users_BankEmployeeId",
			table: "Applications",
			column: "BankEmployeeId",
			principalTable: "Users",
			principalColumn: "Id",
			onDelete: ReferentialAction.SetNull);
	}
}