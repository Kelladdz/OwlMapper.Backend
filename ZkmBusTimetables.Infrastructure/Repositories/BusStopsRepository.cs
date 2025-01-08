using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using ZkmBusTimetables.Core.Interfaces;
using ZkmBusTimetables.Core.Models;
using ZkmBusTimetables.Infrastructure.Persistance;

namespace ZkmBusTimetables.Infrastructure.Repositories
{
    public sealed class BusStopsRepository(ZkmDbContext dbContext) : IBusStopsRepository
    {
        public async Task<BusStop> GetAsync(string slug, CancellationToken cancellationToken)
        {
            var busStop = await dbContext.BusStops
                .Where(busStop => busStop.Slug == slug)
                .FirstOrDefaultAsync(cancellationToken);

            var routeStops = await dbContext.RouteStops
                .Where(routeStop => routeStop.BusStopId == busStop.Id)
                .ToListAsync(cancellationToken);


            foreach (var routeStop in routeStops)
            {
                var variant = await dbContext.Variants
                    .Where(variant => variant.Id == routeStop.VariantId)
                    .FirstOrDefaultAsync(cancellationToken);

                var departures = await dbContext.Departures
                    .Where(departure => departure.VariantId == variant.Id)
                    .ToListAsync(cancellationToken);

                var line = await dbContext.Lines
                    .Where(line => line.Id == variant.LineId)
                    .FirstOrDefaultAsync(cancellationToken);

                routeStop.Variant = variant;
                routeStop.Variant.Line = line;
            }

            return busStop;
            
        } 

        public async Task<IEnumerable<BusStop>> GetAllAsync(CancellationToken cancellationToken)
        {
            var busStops = await dbContext.BusStops
                .AsNoTracking()
                .Include(busStop => busStop.RouteStops)
                .ToListAsync(cancellationToken);

            return busStops;
        }

        public async Task<IEnumerable<BusStop>> SearchAsync(string term, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(term)) 
                return Array.Empty<BusStop>();

            var matchingBusStops = await dbContext.BusStops
                .AsNoTracking()
                .Include(busStop => busStop.RouteStops)
                .ThenInclude(routeStop => routeStop.Variant)
                .ThenInclude(variant => variant.Line)
                .Where(x => x.Name.Contains(term) || x.City.Contains(term))
                .ToListAsync();

            return matchingBusStops;
        }

        public async Task<BusStop> InsertAsync(BusStop busStop, CancellationToken cancellationToken)
        {
            var busStopsWithSameNameAndCity = await dbContext.BusStops
                .Where(x => x.Name == busStop.Name && x.City == busStop.City)
                .ToListAsync(cancellationToken);
            if (busStopsWithSameNameAndCity.Count == 1)
            {
                foreach (var stop in busStopsWithSameNameAndCity)
                {
                    stop.GenerateSlug(0);
                }
                busStop.GenerateSlug(1);
            } else busStop.GenerateSlug(null);


            await dbContext.BusStops.AddAsync(busStop, cancellationToken);
            return await dbContext.SaveChangesAsync(cancellationToken) > 0 
                ? busStop : throw new HttpResponseException(HttpStatusCode.BadRequest);
        }

        public async Task<bool> UpdateAsync(string slug, BusStop busStopToUpdate, CancellationToken cancellationToken)
        {
            var busStop = await dbContext.BusStops
                .Where(busStop => busStop.Slug == slug)
                .FirstOrDefaultAsync(cancellationToken);


            dbContext.BusStops.Remove(busStop);

            if (await InsertAsync(busStopToUpdate, cancellationToken) is not null)
            {
                return true;
            }
            else return false;
        }

        public async Task<bool> DeleteAsync(string slug, CancellationToken cancellationToken)
        {
            var busStop = await dbContext.BusStops
                .Where(busStop => busStop.Slug == slug)
                .FirstOrDefaultAsync(cancellationToken);

            dbContext.BusStops.Remove(busStop);

            return await dbContext.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> DeleteAllAsync(CancellationToken cancellationToken)
        {
            var allBusStops = await dbContext.BusStops
                .ToListAsync(cancellationToken);

            dbContext.BusStops.RemoveRange(allBusStops);

            return await dbContext.SaveChangesAsync(cancellationToken) > 0;
        }
    }
}
