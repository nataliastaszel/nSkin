﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using nSkinShop.Data;

#nullable disable

namespace nSkinShop.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241208182720_AddProductsTable")]
    partial class AddProductsTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("nSkinShop.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DisplayOrder")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DisplayOrder = 1,
                            Name = "Face Care Products"
                        },
                        new
                        {
                            Id = 2,
                            DisplayOrder = 2,
                            Name = "Body Care Products"
                        },
                        new
                        {
                            Id = 3,
                            DisplayOrder = 3,
                            Name = "Sun Care Products"
                        },
                        new
                        {
                            Id = 4,
                            DisplayOrder = 4,
                            Name = "Treatment & Specialty Products"
                        });
                });

            modelBuilder.Entity("nSkinShop.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<double>("Price100ml")
                        .HasColumnType("float");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Brand = "Drunk Elephant",
                            Description = "Moisturizing face cream",
                            Price = 85.530000000000001,
                            Price100ml = 116.27,
                            Title = "Protini Polypeptide Cream"
                        },
                        new
                        {
                            Id = 2,
                            Brand = "Clinique",
                            Description = "Nourishing night cream",
                            Price = 100.89,
                            Price100ml = 205.77000000000001,
                            Title = "Smart Clinical Repair Wrinkle Correcting Cream"
                        },
                        new
                        {
                            Id = 3,
                            Brand = "Sol de Janeiro",
                            Description = "Brazilian body cream",
                            Price = 23.550000000000001,
                            Price100ml = 31.34,
                            Title = "Brazilian Bum Bum Cream"
                        },
                        new
                        {
                            Id = 4,
                            Brand = "Rituals",
                            Description = "Shower gel foam",
                            Price = 13.550000000000001,
                            Price100ml = 17.890000000000001,
                            Title = "The Ritual of Ayurveda"
                        },
                        new
                        {
                            Id = 5,
                            Brand = "SuperGoop",
                            Description = "Sun protection filter SPF 30",
                            Price = 24.539999999999999,
                            Price100ml = 50.0,
                            Title = "Glowscreen"
                        },
                        new
                        {
                            Id = 6,
                            Brand = "Clarins",
                            Description = "After sun care",
                            Price = 34.460000000000001,
                            Price100ml = 76.549999999999997,
                            Title = "Soothing After Sun Balm"
                        },
                        new
                        {
                            Id = 7,
                            Brand = "Dr Irena Eris",
                            Description = "Smoothing peeling",
                            Price = 34.460000000000001,
                            Price100ml = 80.060000000000002,
                            Title = "Body Art Smoothing Body Scrub with Alabaster "
                        },
                        new
                        {
                            Id = 8,
                            Brand = "Phlov",
                            Description = "Intensive balm against stretch marks",
                            Price = 27.02,
                            Price100ml = 54.82,
                            Title = "Take control, Babe!"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
