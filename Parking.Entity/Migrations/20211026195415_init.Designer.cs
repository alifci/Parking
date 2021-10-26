﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Parking.Entity.Context;

namespace Parking.Entity.Migrations
{
    [DbContext(typeof(ParkingContext))]
    [Migration("20211026195415_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Parking.Entity.Entity.Car", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<DateTime>("Created");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<DateTime>("Modified");

                    b.Property<string>("PlateNumber")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("Parking.Entity.Entity.Card", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CarId");

                    b.Property<DateTime>("Created");

                    b.Property<decimal>("Credit");

                    b.Property<int>("EmoployeeId");

                    b.Property<DateTime>("Modified");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.HasIndex("CarId")
                        .IsUnique();

                    b.HasIndex("EmoployeeId")
                        .IsUnique();

                    b.ToTable("Cards");
                });

            modelBuilder.Entity("Parking.Entity.Entity.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Age");

                    b.Property<DateTime>("Created");

                    b.Property<DateTime>("Modified");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Parking.Entity.Entity.HighwayGatePassing", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CarId");

                    b.Property<decimal>("Cost");

                    b.Property<DateTime>("Created");

                    b.Property<bool>("IsFree");

                    b.Property<DateTime>("Modified");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.HasIndex("CarId");

                    b.ToTable("HighwayGatePassinges");
                });

            modelBuilder.Entity("Parking.Entity.Entity.Card", b =>
                {
                    b.HasOne("Parking.Entity.Entity.Car", "Car")
                        .WithOne("Card")
                        .HasForeignKey("Parking.Entity.Entity.Card", "CarId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Parking.Entity.Entity.Employee", "Employee")
                        .WithOne("Card")
                        .HasForeignKey("Parking.Entity.Entity.Card", "EmoployeeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Parking.Entity.Entity.HighwayGatePassing", b =>
                {
                    b.HasOne("Parking.Entity.Entity.Car", "Car")
                        .WithMany("HighwayGatePassinges")
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
