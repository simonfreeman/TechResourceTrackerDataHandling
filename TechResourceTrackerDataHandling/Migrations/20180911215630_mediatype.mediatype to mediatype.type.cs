using Microsoft.EntityFrameworkCore.Migrations;

namespace TechResourceTrackerDataHandling.Migrations
{
    public partial class MediaTypeMediaTypeToMediaTypeType : Migration
    {

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MediaType",
                table: "MediaType",
                newName: "Type");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "MediaType",
                newName: "MediaType");
        }
    }
}
