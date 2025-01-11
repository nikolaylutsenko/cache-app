﻿// <auto-generated />
using System;
using Medicine.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Medicine.Migrations.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250111143220_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Medicine.Database.Enteties.Company", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<string>("ContactInfo")
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<string>("Country")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<long>("Version")
                        .IsConcurrencyToken()
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Company", (string)null);
                });

            modelBuilder.Entity("Medicine.Database.Enteties.Ingredient", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<Guid>("MedicineId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("numeric");

                    b.Property<Guid>("SubstanceId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("MedicineId");

                    b.HasIndex("SubstanceId");

                    b.ToTable("Ingredient", (string)null);
                });

            modelBuilder.Entity("Medicine.Database.Enteties.Medicine", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<Guid?>("ManufacturerId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<long>("Version")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ManufacturerId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Medicine", (string)null);
                });

            modelBuilder.Entity("Medicine.Database.Enteties.MedicineSpecification", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Dosage")
                        .HasColumnType("text");

                    b.Property<int?>("Form")
                        .HasColumnType("integer");

                    b.Property<Guid>("MedicineId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("MedicineId")
                        .IsUnique();

                    b.ToTable("MedicineSpecification");
                });

            modelBuilder.Entity("Medicine.Database.Enteties.Substance", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<string>("Formula")
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<Guid?>("ManufacturerId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<long>("Version")
                        .IsConcurrencyToken()
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ManufacturerId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Substance", (string)null);
                });

            modelBuilder.Entity("Medicine.Database.Enteties.Tag", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<long>("Version")
                        .IsConcurrencyToken()
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Tag", (string)null);
                });

            modelBuilder.Entity("Medicine_Tags", b =>
                {
                    b.Property<Guid>("TagId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("MedicineId")
                        .HasColumnType("uuid");

                    b.HasKey("TagId", "MedicineId");

                    b.HasIndex("MedicineId");

                    b.ToTable("Medicine_Tags");
                });

            modelBuilder.Entity("Medicine.Database.Enteties.Ingredient", b =>
                {
                    b.HasOne("Medicine.Database.Enteties.Medicine", "Medicine")
                        .WithMany("Ingredients")
                        .HasForeignKey("MedicineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Medicine.Database.Enteties.Substance", "Substance")
                        .WithMany("Ingredients")
                        .HasForeignKey("SubstanceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Medicine");

                    b.Navigation("Substance");
                });

            modelBuilder.Entity("Medicine.Database.Enteties.Medicine", b =>
                {
                    b.HasOne("Medicine.Database.Enteties.Company", "Manufacturer")
                        .WithMany()
                        .HasForeignKey("ManufacturerId");

                    b.Navigation("Manufacturer");
                });

            modelBuilder.Entity("Medicine.Database.Enteties.MedicineSpecification", b =>
                {
                    b.HasOne("Medicine.Database.Enteties.Medicine", "Medicine")
                        .WithOne("Specification")
                        .HasForeignKey("Medicine.Database.Enteties.MedicineSpecification", "MedicineId");

                    b.Navigation("Medicine");
                });

            modelBuilder.Entity("Medicine.Database.Enteties.Substance", b =>
                {
                    b.HasOne("Medicine.Database.Enteties.Company", "Manufacturer")
                        .WithMany("Substances")
                        .HasForeignKey("ManufacturerId");

                    b.Navigation("Manufacturer");
                });

            modelBuilder.Entity("Medicine_Tags", b =>
                {
                    b.HasOne("Medicine.Database.Enteties.Medicine", null)
                        .WithMany()
                        .HasForeignKey("MedicineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Medicine.Database.Enteties.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Medicine.Database.Enteties.Company", b =>
                {
                    b.Navigation("Substances");
                });

            modelBuilder.Entity("Medicine.Database.Enteties.Medicine", b =>
                {
                    b.Navigation("Ingredients");

                    b.Navigation("Specification");
                });

            modelBuilder.Entity("Medicine.Database.Enteties.Substance", b =>
                {
                    b.Navigation("Ingredients");
                });
#pragma warning restore 612, 618
        }
    }
}
