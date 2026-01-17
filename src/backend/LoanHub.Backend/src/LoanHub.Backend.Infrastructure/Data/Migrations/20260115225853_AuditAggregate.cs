using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoanHub.Backend.Infrastructure.Data.Migrations;

/// <inheritdoc />
public partial class AuditAggregate : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.CreateTable(
			name: "Audit",
			columns: table =>
			{
				return new
				{
					Id = table.Column<int>(type: "int", nullable: false)
										.Annotation("SqlServer:Identity", "1, 1"),
					Method = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Path = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Body = table.Column<string>(type: "nvarchar(max)", nullable: true),
					HeadersJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
					ParamsJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
					QueryParamsJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
					StatusCode = table.Column<int>(type: "int", nullable: false),
					DurationMs = table.Column<long>(type: "bigint", nullable: false),
					Error = table.Column<string>(type: "nvarchar(max)", nullable: true),
					CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
					DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
					IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
				};
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_Audit", x => x.Id);
			});
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropTable(
			name: "Audit");
	}
}