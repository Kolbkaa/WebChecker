﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebChecker.Database;

namespace WebChecker.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebChecker.Database.Entity.ProductEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CheckDate");

                    b.Property<string>("Link");

                    b.Property<string>("Name");

                    b.Property<int?>("PriceId");

                    b.HasKey("Id");

                    b.HasIndex("PriceId");

                    b.ToTable("ProductEntity");
                });

            modelBuilder.Entity("WebChecker.Database.Entity.WebsiteEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CartButtonXPatch");

                    b.Property<string>("MainUrl");

                    b.Property<string>("NameXPath");

                    b.Property<string>("PriceXPath");

                    b.HasKey("Id");

                    b.ToTable("WebsiteEntities");
                });

            modelBuilder.Entity("WebChecker.Model.Price", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("Dec");

                    b.Property<int?>("Unity");

                    b.HasKey("Id");

                    b.ToTable("Price");
                });

            modelBuilder.Entity("WebChecker.Database.Entity.ProductEntity", b =>
                {
                    b.HasOne("WebChecker.Model.Price", "Price")
                        .WithMany()
                        .HasForeignKey("PriceId");
                });
#pragma warning restore 612, 618
        }
    }
}
