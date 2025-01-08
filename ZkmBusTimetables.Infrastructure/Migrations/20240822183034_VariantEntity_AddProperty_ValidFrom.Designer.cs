﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ZkmBusTimetables.Infrastructure.Persistance;

#nullable disable

namespace ZkmBusTimetables.Infrastructure.Migrations
{
    [DbContext(typeof(ZkmDbContext))]
    [Migration("20240822183034_VariantEntity_AddProperty_ValidFrom")]
    partial class VariantEntity_AddProperty_ValidFrom
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ZkmBusTimetables.Core.Models.Address", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AddressString")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)")
                        .HasColumnName("AddressString");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("City");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)")
                        .HasColumnName("Number");

                    b.Property<string>("Street")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("Street");

                    b.HasKey("Id");

                    b.HasIndex("AddressString");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("ZkmBusTimetables.Core.Models.BusStop", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("City");

                    b.Property<bool>("IsRequest")
                        .HasColumnType("bit")
                        .HasColumnName("IsRequest");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("Name");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("Slug");

                    b.HasKey("Id");

                    b.HasIndex("City");

                    b.HasIndex("Name");

                    b.ToTable("BusStops");
                });

            modelBuilder.Entity("ZkmBusTimetables.Core.Models.Departure", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<int>("ScheduleDay")
                        .HasColumnType("int")
                        .HasColumnName("ScheduleDay");

                    b.Property<TimeOnly>("Time")
                        .HasColumnType("time")
                        .HasColumnName("Time");

                    b.Property<Guid>("VariantId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("VariantId");

                    b.ToTable("Departures");
                });

            modelBuilder.Entity("ZkmBusTimetables.Core.Models.Line", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.ToTable("Lines");
                });

            modelBuilder.Entity("ZkmBusTimetables.Core.Models.RouteLinePoint", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsManuallyAdded")
                        .HasColumnType("bit")
                        .HasColumnName("IsManuallyAdded");

                    b.Property<int>("Order")
                        .HasColumnType("int")
                        .HasColumnName("Order");

                    b.Property<Guid>("VariantId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("VariantId");

                    b.ToTable("RouteLinePoints");
                });

            modelBuilder.Entity("ZkmBusTimetables.Core.Models.RouteStop", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("BusStopId")
                        .HasColumnType("int");

                    b.Property<int>("Order")
                        .HasColumnType("int")
                        .HasColumnName("Order");

                    b.Property<int>("TimeToTravelInMinutes")
                        .HasColumnType("int")
                        .HasColumnName("TravelTimeInMinutes");

                    b.Property<Guid>("VariantId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("BusStopId");

                    b.HasIndex("VariantId");

                    b.ToTable("RouteStops");
                });

            modelBuilder.Entity("ZkmBusTimetables.Core.Models.Variant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<Guid>("LineId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(10)")
                        .HasColumnName("Name");

                    b.Property<string>("Route")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("Route");

                    b.Property<DateOnly>("ValidFrom")
                        .HasColumnType("date")
                        .HasColumnName("Valid From");

                    b.HasKey("Id");

                    b.HasIndex("LineId");

                    b.ToTable("Variants");
                });

            modelBuilder.Entity("ZkmBusTimetables.Core.Models.BusStop", b =>
                {
                    b.OwnsOne("ZkmBusTimetables.Core.Models.Coordinate", "Coordinate", b1 =>
                        {
                            b1.Property<int>("BusStopId")
                                .HasColumnType("int");

                            b1.Property<double>("Lat")
                                .HasColumnType("float");

                            b1.Property<double>("Lng")
                                .HasColumnType("float");

                            b1.HasKey("BusStopId");

                            b1.ToTable("BusStops");

                            b1.WithOwner()
                                .HasForeignKey("BusStopId");
                        });

                    b.Navigation("Coordinate")
                        .IsRequired();
                });

            modelBuilder.Entity("ZkmBusTimetables.Core.Models.Departure", b =>
                {
                    b.HasOne("ZkmBusTimetables.Core.Models.Variant", "Variant")
                        .WithMany("Departures")
                        .HasForeignKey("VariantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Variant");
                });

            modelBuilder.Entity("ZkmBusTimetables.Core.Models.RouteLinePoint", b =>
                {
                    b.HasOne("ZkmBusTimetables.Core.Models.Variant", "Variant")
                        .WithMany("RouteLinePoints")
                        .HasForeignKey("VariantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("ZkmBusTimetables.Core.Models.Coordinate", "Coordinate", b1 =>
                        {
                            b1.Property<Guid>("RouteLinePointId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<double>("Lat")
                                .HasColumnType("float");

                            b1.Property<double>("Lng")
                                .HasColumnType("float");

                            b1.HasKey("RouteLinePointId");

                            b1.ToTable("RouteLinePoints");

                            b1.WithOwner()
                                .HasForeignKey("RouteLinePointId");
                        });

                    b.Navigation("Coordinate")
                        .IsRequired();

                    b.Navigation("Variant");
                });

            modelBuilder.Entity("ZkmBusTimetables.Core.Models.RouteStop", b =>
                {
                    b.HasOne("ZkmBusTimetables.Core.Models.BusStop", "BusStop")
                        .WithMany("RouteStops")
                        .HasForeignKey("BusStopId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ZkmBusTimetables.Core.Models.Variant", "Variant")
                        .WithMany("RouteStops")
                        .HasForeignKey("VariantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BusStop");

                    b.Navigation("Variant");
                });

            modelBuilder.Entity("ZkmBusTimetables.Core.Models.Variant", b =>
                {
                    b.HasOne("ZkmBusTimetables.Core.Models.Line", "Line")
                        .WithMany("Variants")
                        .HasForeignKey("LineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Line");
                });

            modelBuilder.Entity("ZkmBusTimetables.Core.Models.BusStop", b =>
                {
                    b.Navigation("RouteStops");
                });

            modelBuilder.Entity("ZkmBusTimetables.Core.Models.Line", b =>
                {
                    b.Navigation("Variants");
                });

            modelBuilder.Entity("ZkmBusTimetables.Core.Models.Variant", b =>
                {
                    b.Navigation("Departures");

                    b.Navigation("RouteLinePoints");

                    b.Navigation("RouteStops");
                });
#pragma warning restore 612, 618
        }
    }
}
