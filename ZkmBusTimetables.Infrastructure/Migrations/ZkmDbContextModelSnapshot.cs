﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ZkmBusTimetables.Infrastructure.Persistance;

#nullable disable

namespace ZkmBusTimetables.Infrastructure.Migrations
{
    [DbContext(typeof(ZkmDbContext))]
    partial class ZkmDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("ZkmBusTimetables.Core.Models.Address", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AddressString")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("AddressString");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("City");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)")
                        .HasColumnName("Number");

                    b.Property<string>("Street")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Street");

                    b.HasKey("Id");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("ZkmBusTimetables.Core.Models.ApplicationRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("ZkmBusTimetables.Core.Models.ApplicationUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
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
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("City");

                    b.Property<bool>("IsRequest")
                        .HasColumnType("bit")
                        .HasColumnName("IsRequest");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
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

                    b.Property<bool>("IsOnlyInDaysWithoutSchool")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasColumnName("IsOnlyInDaysWithoutSchool");

                    b.Property<bool>("IsOnlyInSchoolDays")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasColumnName("IsOnlyInSchoolDays");

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

                    b.Property<bool>("IsDefault")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasColumnName("Is Default");

                    b.Property<Guid>("LineId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Route")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Route");

                    b.Property<string>("Slug")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)")
                        .HasColumnName("Slug");

                    b.Property<DateOnly>("ValidFrom")
                        .HasColumnType("date")
                        .HasColumnName("Valid From");

                    b.HasKey("Id");

                    b.HasIndex("LineId");

                    b.ToTable("Variants");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("ZkmBusTimetables.Core.Models.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("ZkmBusTimetables.Core.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("ZkmBusTimetables.Core.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("ZkmBusTimetables.Core.Models.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ZkmBusTimetables.Core.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("ZkmBusTimetables.Core.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
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
