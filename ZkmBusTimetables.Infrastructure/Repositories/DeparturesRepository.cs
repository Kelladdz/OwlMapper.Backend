using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Web.Http;
using ZkmBusTimetables.Core.Enums;
using ZkmBusTimetables.Core.Interfaces;
using ZkmBusTimetables.Core.Models;
using ZkmBusTimetables.Infrastructure.Persistance;

namespace ZkmBusTimetables.Infrastructure.Repositories
{
    public class DeparturesRepository(ZkmDbContext dbContext) : IDeparturesRepository
    {
        public async Task<IEnumerable<Departure>> GetByBusStopIdAsync(int busStopId, string lineName, IList<ScheduleDay> scheduleDayValues, int page, int pageSize, CancellationToken cancellationToken)
        {
            List<Departure> restDepartures = new List<Departure>();
            var now = TimeOnly.FromDateTime(DateTime.Now);

            var departures = await dbContext.Departures
                .Include(departure => departure.Variant)
                .ThenInclude(variant => variant.Line)
                .Include(departure => departure.Variant)
                .ThenInclude(variant => variant.RouteStops)
                .ThenInclude(routeStop => routeStop.BusStop)
                .AsNoTracking()
                .AsSplitQuery()
                .Where(departure => departure.Variant.RouteStops.Any(routeStop => routeStop.BusStop.Id == busStopId && departure.Variant.RouteStops.OrderByDescending(obj => obj.Order).FirstOrDefault()!.BusStop.Id != busStopId) && lineName == departure.Variant.Line.Name && departure.ScheduleDay == scheduleDayValues[0])
                .Select(departure => new Departure
                {
                    Time = departure.Time.AddMinutes(departure.Variant.RouteStops.Where(routeStop => routeStop.BusStopId == busStopId).FirstOrDefault()!.TimeToTravelInMinutes),
                    VariantId = departure.VariantId,
                    Variant = new Variant
                    {
                        Line = departure.Variant.Line,
                        LineId = departure.Variant.Line.Id,
                        Route = departure.Variant.Route,
                        IsDefault = departure.Variant.IsDefault,
                        RouteStops = new[] { departure.Variant.RouteStops.OrderByDescending(obj => obj.Order).FirstOrDefault()! },
                        RouteLinePoints = departure.Variant.RouteLinePoints.OrderBy(obj => obj.Order).ToList()
                    },

                    ScheduleDay = scheduleDayValues[0],
                    IsOnlyInSchoolDays = departure.IsOnlyInSchoolDays,
                    IsOnlyInDaysWithoutSchool = departure.IsOnlyInDaysWithoutSchool
                })
                .OrderBy(departure => departure.Time.Hour * 60 + departure.Time.Minute)
                .ToListAsync(cancellationToken)
                ?? throw new HttpResponseException(HttpStatusCode.NotFound);

            var today = DateTime.Today;

            if (today.Month != 7 && today.Month != 8)
            {
                departures = departures.Where(departure => departure.IsOnlyInSchoolDays || (!departure.IsOnlyInSchoolDays && !departure.IsOnlyInDaysWithoutSchool)).ToList();
            }
            else
            {
                departures = departures.Where(departure => departure.IsOnlyInDaysWithoutSchool || (!departure.IsOnlyInSchoolDays && !departure.IsOnlyInDaysWithoutSchool)).ToList();
            }

            if (pageSize != 0 && departures.Count < pageSize)
            {
                var remainingSlots = pageSize - departures.Count;
                restDepartures = await dbContext.Departures
                .Include(departure => departure.Variant)
                .ThenInclude(variant => variant.Line)
                .Include(departure => departure.Variant)
                .ThenInclude(variant => variant.RouteStops)
                .ThenInclude(routeStop => routeStop.BusStop)
                .AsNoTracking()
                .AsSplitQuery()
                .Where(departure => departure.Variant.RouteStops.Any(routeStop => routeStop.BusStop.Id == busStopId && departure.Variant.RouteStops.OrderByDescending(obj => obj.Order).FirstOrDefault()!.BusStop.Id != busStopId) && lineName == departure.Variant.Line.Name && departure.ScheduleDay == scheduleDayValues[1])
                .Select(departure => new Departure
                {
                    Time = departure.Time.AddMinutes(departure.Variant.RouteStops.Where(routeStop => routeStop.BusStopId == busStopId).FirstOrDefault()!.TimeToTravelInMinutes),
                    VariantId = departure.VariantId,
                    Variant = new Variant
                    {
                        Line = new Line
                        {
                            Name = departure.Variant.Line.Name
                        },
                        Route = departure.Variant.Route,
                        IsDefault = departure.Variant.IsDefault,
                        RouteStops = new[] { departure.Variant.RouteStops.OrderByDescending(obj => obj.Order).FirstOrDefault()! },
                        RouteLinePoints = (ICollection<RouteLinePoint>)departure.Variant.RouteLinePoints.Select(routeLinePoint => new RouteLinePoint
                        {
                            Coordinate = routeLinePoint.Coordinate,
                            Order = routeLinePoint.Order,
                            IsManuallyAdded = routeLinePoint.IsManuallyAdded
                        })
                    },
                    ScheduleDay = scheduleDayValues[1],
                    IsOnlyInSchoolDays = departure.IsOnlyInSchoolDays,
                    IsOnlyInDaysWithoutSchool = departure.IsOnlyInDaysWithoutSchool
                })
                .OrderBy(departure => departure.Time.Hour * 60 + departure.Time.Minute)
                .Take(remainingSlots)
                .ToListAsync(cancellationToken)
                ?? throw new HttpResponseException(HttpStatusCode.NotFound);


                var totalCount = restDepartures.Count == 0 ? departures.Count : departures.Count + restDepartures.Count;
                departures.AddRange(restDepartures);
                var totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);
                var departuresPerPage = departures.Where(departure => departure.Time > now).Skip((page - 1) * pageSize).Take(pageSize).ToList();

                return departuresPerPage;
            }
            /*else if (pageSize == 0 && departures.Count < 10)
            {
                restDepartures = await dbContext.Departures
                .Include(departure => departure.Variant)
                .ThenInclude(variant => variant.Line)
                .Include(departure => departure.Variant)
                .ThenInclude(variant => variant.RouteStops)
                .ThenInclude(routeStop => routeStop.BusStop)
                .AsNoTracking()
                .AsSplitQuery()
                .Where(departure => departure.Variant.RouteStops.Any(routeStop => routeStop.BusStop.Id == busStopId && departure.Variant.RouteStops.OrderByDescending(obj => obj.Order).FirstOrDefault()!.BusStop.Id != busStopId) && lineName == departure.Variant.Line.Name && departure.ScheduleDay == scheduleDayValues[1])
                .Select(departure => new Departure
                {
                    Time = departure.Time.AddMinutes(departure.Variant.RouteStops.Where(routeStop => routeStop.BusStopId == busStopId).FirstOrDefault()!.TimeToTravelInMinutes),
                    VariantId = departure.VariantId,
                    Variant = new Variant
                    {
                        Line = new Line
                        {
                            Name = departure.Variant.Line.Name
                        },
                        Route = departure.Variant.Route,
                        IsDefault = departure.Variant.IsDefault,
                        RouteStops = new[] { departure.Variant.RouteStops.OrderByDescending(obj => obj.Order).FirstOrDefault()! },
                        RouteLinePoints = (ICollection<RouteLinePoint>)departure.Variant.RouteLinePoints.Select(routeLinePoint => new RouteLinePoint
                        {
                            Coordinate = routeLinePoint.Coordinate,
                            Order = routeLinePoint.Order,
                            IsManuallyAdded = routeLinePoint.IsManuallyAdded
                        })
                    },
                    ScheduleDay = scheduleDayValues[1],
                    IsOnlyInSchoolDays = departure.IsOnlyInSchoolDays,
                    IsOnlyInDaysWithoutSchool = departure.IsOnlyInDaysWithoutSchool
                })
                .OrderBy(departure => departure.Time.Hour * 60 + departure.Time.Minute)
                .ToListAsync(cancellationToken)
                ?? throw new HttpResponseException(HttpStatusCode.NotFound);

                departures.AddRange(restDepartures);

                return departures;
            } */
            else if (pageSize != 0 && pageSize <= departures.Count)
            {
                return departures.Take(20);
            }
            else
            {
                return departures;
            }
        }
    }
}