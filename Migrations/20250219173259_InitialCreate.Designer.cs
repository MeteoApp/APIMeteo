﻿// <auto-generated />
using System;
using APIMeteo.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace APIMeteo.Migrations
{
    [DbContext(typeof(EntityFrameworkDbContext))]
    [Migration("20250219173259_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("APIMeteo.Models.Alert", b =>
                {
                    b.Property<int>("AlertId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("AlertId"));

                    b.Property<string>("AlertMessage")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("MeasureId")
                        .HasColumnType("integer");

                    b.HasKey("AlertId");

                    b.HasIndex("MeasureId");

                    b.ToTable("Alerts");
                });

            modelBuilder.Entity("APIMeteo.Models.Measure", b =>
                {
                    b.Property<int>("MeasureId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("MeasureId"));

                    b.Property<double>("Altitude")
                        .HasColumnType("double precision");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<double>("Humidity")
                        .HasColumnType("double precision");

                    b.Property<double>("Pressure")
                        .HasColumnType("double precision");

                    b.Property<int>("RoomId")
                        .HasColumnType("integer");

                    b.Property<double>("Temperature")
                        .HasColumnType("double precision");

                    b.HasKey("MeasureId");

                    b.HasIndex("RoomId");

                    b.ToTable("Measures");
                });

            modelBuilder.Entity("APIMeteo.Models.Room", b =>
                {
                    b.Property<int>("RoomId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("RoomId"));

                    b.Property<string>("RoomName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("RoomId");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("APIMeteo.Models.Alert", b =>
                {
                    b.HasOne("APIMeteo.Models.Measure", "Measure")
                        .WithMany()
                        .HasForeignKey("MeasureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Measure");
                });

            modelBuilder.Entity("APIMeteo.Models.Measure", b =>
                {
                    b.HasOne("APIMeteo.Models.Room", "Room")
                        .WithMany()
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Room");
                });
#pragma warning restore 612, 618
        }
    }
}
