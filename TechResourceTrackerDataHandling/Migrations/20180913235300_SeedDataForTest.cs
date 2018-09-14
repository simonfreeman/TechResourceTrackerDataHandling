using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechResourceTrackerDataHandling.Migrations
{
    public partial class SeedDataForTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeedItems");

            migrationBuilder.CreateTable(
                name: "FeedItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FeedID = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 500, nullable: false),
                    ItemContent = table.Column<string>(nullable: false),
                    DatePublished = table.Column<DateTime>(type: "datetime", nullable: false),
                    Seen = table.Column<bool>(nullable: false),
                    Url = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeedItem_Feed",
                        column: x => x.FeedID,
                        principalTable: "Feed",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Feed",
                columns: new[] { "Id", "Image", "LastUpdated", "MediaTypeId", "Title", "Url" },
                values: new object[] { 1, "https://static.giantbomb.com/uploads/original/11/110673/2894068-3836779617-28773.png", new DateTime(2018, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Giant BeastCast", "https://www.giantbomb.com/podcast-xml/beastcast/" });

            migrationBuilder.InsertData(
                table: "Feed",
                columns: new[] { "Id", "Image", "LastUpdated", "MediaTypeId", "Title", "Url" },
                values: new object[] { 2, "https://msdnshared.blob.core.windows.net/media/2017/10/Microsoft-favicon-cropped3.png", new DateTime(2018, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, ".NET Blog", "https://blogs.msdn.microsoft.com/dotnet/feed/" });

            migrationBuilder.InsertData(
                table: "Feed",
                columns: new[] { "Id", "Image", "LastUpdated", "MediaTypeId", "Title", "Url" },
                values: new object[] { 3, "https://yt3.ggpht.com/-mW8hwFBRUNs/AAAAAAAAAAI/AAAAAAAAAAA/jVL9TZ64o7A/s46-c-k-no-mo-rj-c0xffffff/photo.jpg", new DateTime(2018, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "LearnCode.academy", "https://www.youtube.com/feeds/videos.xml?channel_id=UCVTlvUkGslCV_h-nSAId8Sw" });

            migrationBuilder.InsertData(
                table: "FeedItem",
                columns: new[] { "Id", "DatePublished", "FeedID", "ItemContent", "Seen", "Title", "Url" },
                values: new object[,]
                {
                    { 1, new DateTime(2018, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "We've got some updated impressions on Dreams and Spider-Man! Also, we explore some of the key differences between Gremlins and Gremlins 2, Abby's love of the Beach Boys, and more from the world of video games!", true, "The Giant Beastcast - Episode 165", "Ep165_-_The_Giant_Beastcast-07-19-2018-4836496507.mp3" },
                    { 2, new DateTime(2018, 7, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "More from the Warhammer 40K universe, the No Man's Sky universe, and the universe of things Alex hates talking about (mostly bathrooms). We've also got the news, your emails, and some stellar pun work in this extraordinary episode.", false, "The Giant Beastcast - Episode 166", "Ep166_-_The_Giant_Beastcast-07-26-2018-1461758603.mp3" },
                    { 3, new DateTime(2018, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, @"In part 2 of our machine learning crash course, we'll make a recommendation engine in the browser.
                                        Here's the code! 
                                        https://codepen.io/willrstern/pen/WzZqpd

                                        We will make a recommendation engine example that fully runs in the browser with really good performance.  Artificial intelligence and machine learning are awesome new frontiers in computing and data science.  Web developers can use machine learning and artificial intelligence to solve problems in fun and unique ways.

                                        Once we build a trained model using our neural network, we can quickly run thousands of possible options through our model to get ideal recommendations for our users.", true, "Machine Learning Tutorial - Making a recommendation engine IN THE BROWSER", "https://www.youtube.com/watch?v=lvzekeBQsSo" },
                    { 4, new DateTime(2018, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, @"GET THE SOURCE CODE: https://github.com/learncodeacademy/vue-tutorials

                                        Now that we have the basics of Vue.js, let's scale it with a full build system and .vue single-file-components.  This is where Vue gets really fun in my opinion.

                                        The Vue framework is extremely simple to learn and fun to use.
                                        It's my tool of choice for small widgets and simple apps, but can easily scale to large application development.
                                        Vuejs is also the best framework for new JS developers to learn when starting out.

                                        Vue.js 1: https://www.youtube.com/watch?v=mZY1yyrlJWU&amp;index=1&amp;list=PLoYCgNOIyGADZuvKJweutZDOO9VI9YiJ9
                                        Vue.js 2: https://www.youtube.com/watch?v=h6lhOYv-QM4&amp;index=2&amp;list=PLoYCgNOIyGADZuvKJweutZDOO9VI9YiJ9
                                        Vue.js 3: https://www.youtube.com/watch?v=t0w2KLOLaTA&amp;index=3&amp;list=PLoYCgNOIyGADZuvKJweutZDOO9VI9YiJ9
                                        Vue.js 4: https://www.youtube.com/watch?v=1V9Lcnm1Dqw&amp;index=4&amp;list=PLoYCgNOIyGADZuvKJweutZDOO9VI9YiJ9
                                        Vue.js 5: https://www.youtube.com/watch?v=inJDWcHmsss&amp;list=PLoYCgNOIyGADZuvKJweutZDOO9VI9YiJ9&amp;index=6
                                        Vue.js 6: https://www.youtube.com/watch?v=Oyr5X5HwXhM&amp;list=PLoYCgNOIyGADZuvKJweutZDOO9VI9YiJ9&amp;index=5
                                        Vue.js 7: https://www.youtube.com/watch?v=IkcJ0YAiycQ&amp;list=PLoYCgNOIyGADZuvKJweutZDOO9VI9YiJ9&amp;index=7
                                        Vue.js 8: https://www.youtube.com/watch?v=mY2MiaYiSdw&amp;list=PLoYCgNOIyGADZuvKJweutZDOO9VI9YiJ9&amp;index=8
                                        Vue.js 9: https://www.youtube.com/watch?v=mS9-fTrgjrA&amp;list=PLoYCgNOIyGADZuvKJweutZDOO9VI9YiJ9&amp;index=9", true, "Vue.js Tutorial #7 - Scaling Vue with Single File Components", "https://www.youtube.com/watch?v=IkcJ0YAiycQ" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_FeedItem_FeedID",
                table: "FeedItem",
                column: "FeedID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeedItem");

            migrationBuilder.DeleteData(
                table: "Feed",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Feed",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Feed",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.CreateTable(
                name: "FeedItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DatePublished = table.Column<DateTime>(type: "datetime", nullable: false),
                    FeedID = table.Column<int>(nullable: false),
                    ItemContent = table.Column<string>(nullable: false),
                    Seen = table.Column<bool>(nullable: false),
                    Title = table.Column<string>(maxLength: 500, nullable: false),
                    Url = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeedItems_Feed",
                        column: x => x.FeedID,
                        principalTable: "Feed",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FeedItems_FeedID",
                table: "FeedItems",
                column: "FeedID");
        }
    }
}
