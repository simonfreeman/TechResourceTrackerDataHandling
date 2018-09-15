using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechResourceTrackerDataHandling.Migrations
{
    public partial class CspReporting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CspReport",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DocumentUri = table.Column<string>(nullable: true),
                    Referrer = table.Column<string>(nullable: true),
                    BlockedUri = table.Column<string>(nullable: true),
                    ViolatedDirective = table.Column<string>(nullable: true),
                    EffectiveDirective = table.Column<string>(nullable: true),
                    OriginalPolicy = table.Column<string>(nullable: true),
                    Disposition = table.Column<string>(nullable: true),
                    SourceFile = table.Column<string>(nullable: true),
                    ScriptSample = table.Column<string>(nullable: true),
                    StatusCode = table.Column<int>(nullable: false),
                    LineNumber = table.Column<int>(nullable: false),
                    ColumnNumber = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CspReport", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CspReport");
        }
    }
}
