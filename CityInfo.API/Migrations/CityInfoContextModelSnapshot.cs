﻿// <auto-generated />
using CityInfo.API.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CityInfo.API.Migrations
{
    [DbContext(typeof(CityInfoContext))]
    partial class CityInfoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.26");

            modelBuilder.Entity("CityInfo.API.Entities.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Cities");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Great place",
                            Name = "New York"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Capital City",
                            Name = "Delhi"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Denmark capital",
                            Name = "Copenhegan"
                        });
                });

            modelBuilder.Entity("CityInfo.API.Entities.PointOfInterest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CityId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("PointOfInterest");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CityId = 1,
                            Description = "A large urban park with walking trails, picnic areas, and a beautiful lake.",
                            Name = "City Park"
                        },
                        new
                        {
                            Id = 2,
                            CityId = 1,
                            Description = "Explore the city's rich history through artifacts, exhibits, and interactive displays.",
                            Name = "Historic Museum"
                        },
                        new
                        {
                            Id = 3,
                            CityId = 3,
                            Description = "The Little Mermaid sits on a rock by the waterside at the Langelinie promenade.",
                            Name = "The Little Mermaid"
                        },
                        new
                        {
                            Id = 4,
                            CityId = 3,
                            Description = "Tivoli offers a mix of historic charm and modern entertainment.",
                            Name = "Tivoli Gardens"
                        },
                        new
                        {
                            Id = 5,
                            CityId = 2,
                            Description = "Historic monument.",
                            Name = "Red Fort"
                        });
                });

            modelBuilder.Entity("CityInfo.API.Entities.PointOfInterest", b =>
                {
                    b.HasOne("CityInfo.API.Entities.City", "City")
                        .WithMany("PointOfInterests")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("CityInfo.API.Entities.City", b =>
                {
                    b.Navigation("PointOfInterests");
                });
#pragma warning restore 612, 618
        }
    }
}
