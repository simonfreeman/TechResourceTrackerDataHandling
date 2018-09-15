using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechResourceTrackerDataHandling.Migrations
{
    public partial class DateViolationForCspReports : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateViolated",
                table: "CspReport",
                type: "datetime",
                nullable: false,
                defaultValueSql: "getdate()");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateViolated",
                table: "CspReport");
        }
    }
}
