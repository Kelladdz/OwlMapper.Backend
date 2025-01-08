using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZkmBusTimetables.Core.Enums;
using ZkmBusTimetables.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNet.Identity;
using System.Security.Claims;


namespace ZkmBusTimetables.Infrastructure.Persistance
{
    public class ZkmDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {

        public ZkmDbContext(DbContextOptions<ZkmDbContext> options) : base(options)
        {

        }
        public virtual DbSet<Line> Lines { get; set; }
        public virtual DbSet<Variant> Variants { get; set; }
        public virtual DbSet<Departure> Departures { get; set; }
        public virtual DbSet<BusStop> BusStops { get; set; }
        public virtual DbSet<RouteStop> RouteStops { get; set; }
        public virtual DbSet<RouteLinePoint> RouteLinePoints { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<ApplicationUser> Users { get; set; }

        /*public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            if (!_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                return base.SaveChangesAsync(cancellationToken);
            }
            else
            {
                var currentUser = _httpContextAccessor.HttpContext.User;
                AddChangeHistoryEntries(currentUser);
                return base.SaveChangesAsync(cancellationToken);
            }

        }

        private void AddChangeHistoryEntries(ClaimsPrincipal currentUser)
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted)
                .ToList();

            var firstName = currentUser.FindFirst(c => c.Type == "firstName")!.Value;
            var lastName = currentUser.FindFirst(c => c.Type == "lastName")!.Value;


            foreach (var entry in entries)
            {
                if (!entry.Metadata.IsOwned())
                {
                    var entityIdObject = entry.Property("Id").CurrentValue;
                    var entityId = entityIdObject.ToString();
                    var changeHistory = new ChangeHistory
                    {
                        EntityType = entry.Entity.GetType().Name,
                        EntityId = entityId,
                        ChangeType = entry.State.ToString(),
                        ChangedBy = $"{firstName} {lastName}", // Zastąp to rzeczywistym użytkownikiem, np. z kontekstu HTTP
                        ChangeDate = DateTime.UtcNow
                    };

                    ChangeHistory.Add(changeHistory);
                }
                else
                {
                    var changeHistory = new ChangeHistory
                    {
                        EntityType = entry.Entity.GetType().Name,
                        ChangeType = entry.State.ToString(),
                        ChangedBy = $"{firstName} {lastName}", // Zastąp to rzeczywistym użytkownikiem, np. z kontekstu HTTP
                        ChangeDate = DateTime.UtcNow
                    };

                    ChangeHistory.Add(changeHistory);
                }

            }
        }*/

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Line>(line =>
            {
                line.HasKey(line => line.Id);

                line.Property(line => line.Id)
                    .HasColumnName("Id")
                    .HasColumnType("uniqueidentifier")
                    .IsRequired();

                line.Property(line => line.Name)
                    .HasColumnName("Name")
                    .HasMaxLength(100)
                    .HasColumnType("varchar(100)")
                    .IsRequired();

                line.HasMany(line => line.Variants)
                    .WithOne(variant => variant.Line)
                    .HasForeignKey(variant => variant.LineId);

                line.HasIndex(line => line.Name);
            });

            modelBuilder.Entity<Variant>(variant =>
            {
                variant.HasKey(variant => variant.Id);

                variant.Property(variant => variant.Id)
                    .HasColumnName("Id")
                    .HasColumnType("uniqueidentifier")
                    .IsRequired();

                variant.Property(variant => variant.Route)
                    .HasColumnName("Route")
                    .HasColumnType("nvarchar(100)")
                    .IsRequired();

                variant.Property(variant => variant.IsDefault)
                    .HasColumnName("Is Default")
                    .HasColumnType("bit")
                    .HasDefaultValue("false")
                    .IsRequired();

                variant.Property(variant => variant.Slug)
                    .HasColumnName("Slug")
                    .HasMaxLength(200)
                    .HasColumnType("varchar(200)");

                variant.Property(variant => variant.ValidFrom)
                    .HasColumnName("Valid From")
                    .HasColumnType("date")
                    .IsRequired();

                variant.HasMany(variant => variant.RouteStops)
                    .WithOne(routeStops => routeStops.Variant)
                    .HasForeignKey(routeStops => routeStops.VariantId)
                    .OnDelete(DeleteBehavior.Cascade);

                variant.HasMany(variant => variant.Departures)
                    .WithOne(departure => departure.Variant)
                    .HasForeignKey(departure => departure.VariantId)
                    .OnDelete(DeleteBehavior.Cascade);

                variant.HasMany(variant => variant.RouteLinePoints)
                    .WithOne(routeLinePoint => routeLinePoint.Variant)
                    .HasForeignKey(routeLinePoint => routeLinePoint.VariantId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Departure>(departure =>
            {
                departure.HasKey(departure => departure.Id);

                departure.Property(departure => departure.Id)
                    .HasColumnName("Id")
                    .HasColumnType("uniqueidentifier")
                    .IsRequired();

                departure.HasOne(departure => departure.Variant)
                    .WithMany(variant => variant.Departures);

                departure.Property(departure => departure.ScheduleDay)
                    .HasColumnName("ScheduleDay")
                    .HasColumnType("int");

                departure.Property(departure => departure.Time)
                    .HasColumnName("Time")
                    .HasColumnType("time");

                departure.Property(departure => departure.IsOnlyInSchoolDays)
                    .HasColumnName("IsOnlyInSchoolDays")
                    .HasColumnType("bit")
                    .HasDefaultValue(false);

                departure.Property(departure => departure.IsOnlyInDaysWithoutSchool)
                    .HasColumnName("IsOnlyInDaysWithoutSchool")
                    .HasColumnType("bit")
                    .HasDefaultValue(false);
            });

            modelBuilder.Entity<BusStop>(busStop =>
            {
                busStop.HasKey(busStop => busStop.Id);

                busStop.Property(busStop => busStop.Name)
                    .HasColumnName("Name")
                    .HasMaxLength(100)
                    .HasColumnType("nvarchar(100)")
                    .IsRequired();

                busStop.Property(busStop => busStop.City)
                    .HasColumnName("City")
                    .HasMaxLength(100)
                    .HasColumnType("nvarchar(100)")
                    .IsRequired();

                busStop.OwnsOne(busStop => busStop.Coordinate);

                busStop.Property(busStop => busStop.IsRequest)
                    .HasColumnName("IsRequest")
                    .HasColumnType("bit")
                    .IsRequired();

                busStop.Property(busStop => busStop.Slug)
                    .HasColumnName("Slug")
                    .HasMaxLength(100)
                    .HasColumnType("varchar(100)")
                    .IsRequired();

                busStop.HasMany(busStop => busStop.RouteStops)
                    .WithOne(routeStop => routeStop.BusStop);

                busStop.HasIndex(busStop => busStop.Name);
                busStop.HasIndex(busStop => busStop.City);
            });

            modelBuilder.Entity<RouteStop>(routeStop =>
            {
                routeStop.HasKey(routeStop => routeStop.Id);

                routeStop.HasOne(routeStop => routeStop.Variant)
                    .WithMany(variant => variant.RouteStops)
                    .HasForeignKey(routeStop => routeStop.VariantId);
                    

                routeStop.HasOne(routeStop => routeStop.BusStop)
                    .WithMany(busStop => busStop.RouteStops)
                    .HasForeignKey(routeStop => routeStop.BusStopId);

                routeStop.Property(routeStop => routeStop.TimeToTravelInMinutes)
                    .HasColumnName("TravelTimeInMinutes")
                    .HasColumnType("int");

                routeStop.Property(routeStop => routeStop.Order)
                    .HasColumnName("Order")
                    .HasColumnType("int");
            });

            modelBuilder.Entity<RouteLinePoint>(routeLinePoint =>
            {
                routeLinePoint.HasKey(routeLinePoint => routeLinePoint.Id);

                routeLinePoint.HasOne(routeLinePoint => routeLinePoint.Variant)
                    .WithMany(variant => variant.RouteLinePoints);

                routeLinePoint.OwnsOne(routeLinePoint => routeLinePoint.Coordinate);

                routeLinePoint.Property(routeLinePoint => routeLinePoint.IsManuallyAdded)
                    .HasColumnName("IsManuallyAdded")
                    .HasColumnType("bit")
                    .IsRequired();

                routeLinePoint.Property(routeLinePoint => routeLinePoint.Order)
                    .HasColumnName("Order")
                    .HasColumnType("int");
            });

            modelBuilder.Entity<Address>(address =>
            {
                address.HasKey(address => address.Id);

                address.Property(address => address.City)
                    .HasColumnName("City")
                    .HasMaxLength(100)
                    .HasColumnType("nvarchar(100)")
                    .IsRequired();

                address.Property(address => address.Street)
                    .HasColumnName("Street")
                    .HasMaxLength(100)
                    .HasColumnType("nvarchar(100)");

                address.Property(address => address.Number)
                    .HasColumnName("Number")
                    .HasMaxLength(10)
                    .HasColumnType("varchar(10)")
                    .IsRequired();

                address.Property(address => address.AddressString)
                    .HasColumnName("AddressString")
                    .HasMaxLength(200)
                    .HasColumnType("nvarchar(200)")
                    .IsRequired();


            });


        }
    }
}
