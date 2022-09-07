﻿// <auto-generated />
using System;
using Dionysos.Dionysos.Database.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Dionysos.Migrations
{
    [DbContext(typeof(MainDbContext))]
    [Migration("20220823194020_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-preview.7.22376.2")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Dionysos.Database.Article", b =>
                {
                    b.Property<string>("Ean")
                        .HasColumnType("text");

                    b.Property<string>("ArticleEan")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Vendor")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Ean");

                    b.HasIndex("ArticleEan");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("Dionysos.Database.InventoryItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Ean")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Ean");

                    b.ToTable("InventoryItems");
                });

            modelBuilder.Entity("Dionysos.Database.Article", b =>
                {
                    b.HasOne("Dionysos.Database.Article", null)
                        .WithMany("Articles")
                        .HasForeignKey("ArticleEan");
                });

            modelBuilder.Entity("Dionysos.Database.InventoryItem", b =>
                {
                    b.HasOne("Dionysos.Database.Article", "Article")
                        .WithMany()
                        .HasForeignKey("Ean")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Article");
                });

            modelBuilder.Entity("Dionysos.Database.Article", b =>
                {
                    b.Navigation("Articles");
                });
#pragma warning restore 612, 618
        }
    }
}
