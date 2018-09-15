using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using TechResourceTrackerDataHandling.Models;

namespace TechResourceTrackerDataHandling.Models
{
    public partial class TechResourcesContext : DbContext
    {
        public TechResourcesContext()
        {
        }

        public TechResourcesContext(DbContextOptions<TechResourcesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Feed> Feed { get; set; }
        public virtual DbSet<FeedItem> FeedItem { get; set; }
        public virtual DbSet<MediaType> MediaType { get; set; }
        public virtual DbSet<CspReport> CspReport { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Feed>(entity =>
            {
                entity.Property(e => e.Image)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.LastUpdated).HasColumnType("datetime");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.MediaType)
                    .WithMany(p => p.Feed)
                    .HasForeignKey(d => d.MediaTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Feed_Feed");
            });

            modelBuilder.Entity<FeedItem>(entity =>
            {
                entity.Property(e => e.DatePublished).HasColumnType("datetime");

                entity.Property(e => e.FeedId).HasColumnName("FeedID");

                entity.Property(e => e.ItemContent).IsRequired();

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Url).IsRequired();

                entity.HasOne(d => d.Feed)
                    .WithMany(p => p.FeedItems)
                    .HasForeignKey(d => d.FeedId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FeedItem_Feed");
            });

            modelBuilder.Entity<MediaType>(entity =>
            {
                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasColumnName("Type")
                    .HasMaxLength(50);

            });

            modelBuilder.Entity<CspReport>(entity =>
            {
                entity.Property(e => e.DateViolated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");
            });

            //SEED DATA
            //Real data?
            modelBuilder.Entity<MediaType>().HasData(
                new MediaType() { Id = 1, Type = "Audio" },
                new MediaType() { Id = 2, Type = "Video" },
                new MediaType() { Id = 3, Type = "Other" }
            );

            //test data
            modelBuilder.Entity<Feed>().HasData(
                new Feed()
                {
                    Image = @"https://static.giantbomb.com/uploads/original/11/110673/2894068-3836779617-28773.png",
                    Url = @"https://www.giantbomb.com/podcast-xml/beastcast/",
                    LastUpdated = new DateTime(2018, 8, 1),
                    MediaTypeId = 1,
                    Title = @"Giant BeastCast",
                    Id = 1,
                },
                new Feed()
                {
                    Image = @"https://msdnshared.blob.core.windows.net/media/2017/10/Microsoft-favicon-cropped3.png",
                    Url = @"https://blogs.msdn.microsoft.com/dotnet/feed/",
                    LastUpdated = new DateTime(2018, 9, 1),
                    MediaTypeId = 3,
                    Title = @".NET Blog",
                    Id = 2,
                },
                new Feed()
                {
                    Image = @"https://yt3.ggpht.com/-mW8hwFBRUNs/AAAAAAAAAAI/AAAAAAAAAAA/jVL9TZ64o7A/s46-c-k-no-mo-rj-c0xffffff/photo.jpg",
                    Url = @"https://www.youtube.com/feeds/videos.xml?channel_id=UCVTlvUkGslCV_h-nSAId8Sw",
                    LastUpdated = new DateTime(2018, 9, 10),
                    MediaTypeId = 2,
                    Title = @"LearnCode.academy",
                    Id = 3,
                }
            );

            modelBuilder.Entity<FeedItem>().HasData(
                 new FeedItem()
                 {
                     DatePublished = new DateTime(2018, 7, 20),
                     Title = @"The Giant Beastcast - Episode 165",
                     ItemContent = @"We've got some updated impressions on Dreams and Spider-Man! Also, we explore some of the key differences between Gremlins and Gremlins 2, Abby's love of the Beach Boys, and more from the world of video games!",
                     Seen = true,
                     Url = @"Ep165_-_The_Giant_Beastcast-07-19-2018-4836496507.mp3",
                     Id = 1,
                     FeedId = 1
                 },
                new FeedItem()
                {
                    DatePublished = new DateTime(2018, 7, 27),
                    Title = @"The Giant Beastcast - Episode 166",
                    ItemContent = @"More from the Warhammer 40K universe, the No Man's Sky universe, and the universe of things Alex hates talking about (mostly bathrooms). We've also got the news, your emails, and some stellar pun work in this extraordinary episode.",
                    Seen = false,
                    Url = @"Ep166_-_The_Giant_Beastcast-07-26-2018-1461758603.mp3",
                    Id = 2,
                    FeedId = 1
                },
                new FeedItem()
                {
                    DatePublished = new DateTime(2018, 9, 9),
                    Title = @"Machine Learning Tutorial - Making a recommendation engine IN THE BROWSER",
                    ItemContent = @"In part 2 of our machine learning crash course, we'll make a recommendation engine in the browser.
                        Here's the code! 
                        https://codepen.io/willrstern/pen/WzZqpd

                        We will make a recommendation engine example that fully runs in the browser with really good performance.  Artificial intelligence and machine learning are awesome new frontiers in computing and data science.  Web developers can use machine learning and artificial intelligence to solve problems in fun and unique ways.

                        Once we build a trained model using our neural network, we can quickly run thousands of possible options through our model to get ideal recommendations for our users.",
                    Seen = true,
                    Url = "https://www.youtube.com/watch?v=lvzekeBQsSo",
                    Id = 3,
                    FeedId = 3
                },
                new FeedItem()
                {
                    DatePublished = new DateTime(2018, 9, 9),
                    Title = @"Vue.js Tutorial #7 - Scaling Vue with Single File Components",
                    ItemContent = @"GET THE SOURCE CODE: https://github.com/learncodeacademy/vue-tutorials

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
                        Vue.js 9: https://www.youtube.com/watch?v=mS9-fTrgjrA&amp;list=PLoYCgNOIyGADZuvKJweutZDOO9VI9YiJ9&amp;index=9",
                    Seen = true,
                    Url = @"https://www.youtube.com/watch?v=IkcJ0YAiycQ",
                    Id = 4,
                    FeedId = 3
                }
            );
        }
    }
}
