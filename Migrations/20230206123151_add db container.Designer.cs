﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using esabzi.DB;

#nullable disable

namespace esabzi.Migrations
{
    [DbContext(typeof(EsabziContext))]
    [Migration("20230206123151_add db container")]
    partial class adddbcontainer
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("esabzi.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedByUserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedUserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Picture")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "Street #3, House #22, near Data Darbar, Lahore",
                            ContactNo = "+923354058294",
                            CreatedByUserId = "2fd28110-93d0-427d-9207-d55dbca680fa",
                            CreatedDate = new DateTime(2023, 2, 6, 17, 31, 50, 901, DateTimeKind.Local).AddTicks(8548),
                            Email = "shahbazhassan42000@gmail.com",
                            IsActive = true,
                            Name = "Shahbaz",
                            Password = "$2a$11$2kbBDJvsds0ZIx3YNjQeHO.faCzse4g7wxW7dIVwFsHm7VcpdM8ku",
                            Picture = "https://i.ibb.co/HYJWqBc/Whats-App-Image-2022-10-19-at-23-57-52.jpg",
                            Role = "Admin",
                            Username = "shahbaz"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}