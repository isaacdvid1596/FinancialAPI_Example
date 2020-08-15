﻿// <auto-generated />
using System;
using FinancialApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FinancialApp.Data.Migrations
{
    [DbContext(typeof(FinancialAppContext))]
    [Migration("20200815061415_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.7");

            modelBuilder.Entity("FinancialApp.Data.Models.Account", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("Amount")
                        .HasColumnType("REAL");

                    b.Property<string>("Currency")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Account");

                    b.HasData(
                        new
                        {
                            Id = -1L,
                            Amount = 1500.0,
                            Currency = "USD",
                            Name = "Cuenta en dolares 1"
                        },
                        new
                        {
                            Id = -2L,
                            Amount = 1200.0,
                            Currency = "EUR",
                            Name = "Cuenta en euros única"
                        },
                        new
                        {
                            Id = -3L,
                            Amount = 500.0,
                            Currency = "USD",
                            Name = "Cuenta en dolares 2"
                        });
                });

            modelBuilder.Entity("FinancialApp.Data.Models.Transaction", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long>("AccountId")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Amount")
                        .HasColumnType("REAL");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Transaction");

                    b.HasData(
                        new
                        {
                            Id = -1L,
                            AccountId = -1L,
                            Amount = -20.0,
                            Description = "Comida Dennys",
                            TransactionDate = new DateTime(2020, 8, 14, 0, 0, 0, 0, DateTimeKind.Local)
                        },
                        new
                        {
                            Id = -2L,
                            AccountId = -2L,
                            Amount = 1500.0,
                            Description = "Salario",
                            TransactionDate = new DateTime(2020, 8, 15, 0, 0, 0, 0, DateTimeKind.Local)
                        },
                        new
                        {
                            Id = -3L,
                            AccountId = -1L,
                            Amount = -5.0,
                            Description = "Corte de pelo",
                            TransactionDate = new DateTime(2020, 8, 13, 0, 0, 0, 0, DateTimeKind.Local)
                        });
                });

            modelBuilder.Entity("FinancialApp.Data.Models.Transaction", b =>
                {
                    b.HasOne("FinancialApp.Data.Models.Account", "Account")
                        .WithMany("Transactions")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
