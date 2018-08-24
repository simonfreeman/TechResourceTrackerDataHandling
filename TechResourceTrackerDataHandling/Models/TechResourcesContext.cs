using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

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
        public virtual DbSet<FeedItems> FeedItems { get; set; }
        public virtual DbSet<MediaType> MediaType { get; set; }

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

            modelBuilder.Entity<FeedItems>(entity =>
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
                    .HasConstraintName("FK_FeedItems_Feed");
            });

            modelBuilder.Entity<MediaType>(entity =>
            {
                entity.Property(e => e.MediaType1)
                    .IsRequired()
                    .HasColumnName("MediaType")
                    .HasMaxLength(50);
            });
        }
    }
}
