﻿// <auto-generated />
using System;
using BillPayment.Server.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BillPayment.Server.Migrations
{
    [DbContext(typeof(BillPaymentContext))]
    partial class BillPaymentContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BillPayment.Server.Models.EntityModels.AccountPayable", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("FineAmount")
                        .HasColumnType("decimal(12,2)");

                    b.Property<decimal>("FinePercentage")
                        .HasColumnType("decimal(12,2)");

                    b.Property<decimal>("InterestAmount")
                        .HasColumnType("decimal(12,2)");

                    b.Property<decimal>("InterestPercentage")
                        .HasColumnType("decimal(12,2)");

                    b.Property<int>("LateDays")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<decimal>("OriginalAmount")
                        .HasColumnType("decimal(12,2)");

                    b.Property<DateTime>("PaymentDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("decimal(12,2)");

                    b.HasKey("Id");

                    b.ToTable("AccountsPayable");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DueDate = new DateTime(2024, 4, 5, 0, 0, 0, 0, DateTimeKind.Local),
                            FineAmount = 0m,
                            FinePercentage = 0m,
                            InterestAmount = 0m,
                            InterestPercentage = 0m,
                            LateDays = 0,
                            Name = "Light",
                            OriginalAmount = 100m,
                            PaymentDate = new DateTime(2024, 4, 2, 0, 0, 0, 0, DateTimeKind.Local),
                            TotalAmount = 100m
                        },
                        new
                        {
                            Id = 2,
                            DueDate = new DateTime(2024, 3, 31, 0, 0, 0, 0, DateTimeKind.Local),
                            FineAmount = 1m,
                            FinePercentage = 2m,
                            InterestAmount = 0.15m,
                            InterestPercentage = 0.1m,
                            LateDays = 3,
                            Name = "Water",
                            OriginalAmount = 50m,
                            PaymentDate = new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Local),
                            TotalAmount = 51.15m
                        });
                });
#pragma warning restore 612, 618
        }
    }
}