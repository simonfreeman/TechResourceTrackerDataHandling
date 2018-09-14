﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TechResourceTrackerDataHandling.Models;

namespace TechResourceTrackerDataHandling.Migrations
{
    [DbContext(typeof(TechResourcesContext))]
    [Migration("20180827173116_initial(fromDb)")]
    partial class initialfromDb
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TechResourceTrackerDataHandling.Models.Feed", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<DateTime?>("LastUpdated")
                        .HasColumnType("datetime");

                    b.Property<int>("MediaTypeId");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.HasKey("Id");

                    b.HasIndex("MediaTypeId");

                    b.ToTable("Feed");
                });

            modelBuilder.Entity("TechResourceTrackerDataHandling.Models.FeedItems", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DatePublished")
                        .HasColumnType("datetime");

                    b.Property<int>("FeedId")
                        .HasColumnName("FeedID");

                    b.Property<string>("ItemContent")
                        .IsRequired();

                    b.Property<bool>("Seen");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<string>("Url")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("FeedId");

                    b.ToTable("FeedItems");
                });

            modelBuilder.Entity("TechResourceTrackerDataHandling.Models.MediaType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("MediaType1")
                        .IsRequired()
                        .HasColumnName("MediaType")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("MediaType");
                });

            modelBuilder.Entity("TechResourceTrackerDataHandling.Models.Feed", b =>
                {
                    b.HasOne("TechResourceTrackerDataHandling.Models.MediaType", "MediaType")
                        .WithMany("Feed")
                        .HasForeignKey("MediaTypeId")
                        .HasConstraintName("FK_Feed_Feed");
                });

            modelBuilder.Entity("TechResourceTrackerDataHandling.Models.FeedItems", b =>
                {
                    b.HasOne("TechResourceTrackerDataHandling.Models.Feed", "Feed")
                        .WithMany("FeedItems")
                        .HasForeignKey("FeedId")
                        .HasConstraintName("FK_FeedItems_Feed");
                });
#pragma warning restore 612, 618
        }
    }
}