using Microsoft.EntityFrameworkCore.Migrations;

namespace TechResourceTrackerDataHandling.Migrations
{
    public partial class PopulatingMediaTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "MediaType",
                columns: new[] { "Id", "Type" },
                values: new object[] { 1, "Audio" });

            migrationBuilder.InsertData(
                table: "MediaType",
                columns: new[] { "Id", "Type" },
                values: new object[] { 2, "Video" });

            migrationBuilder.InsertData(
                table: "MediaType",
                columns: new[] { "Id", "Type" },
                values: new object[] { 3, "Other" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MediaType",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MediaType",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "MediaType",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
