using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZkmBusTimetables.Infrastructure.Persistance;
using ZkmBusTimetables.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using ZkmBusTimetables.Core.Models;
using System.Net;
using System.Web.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.VisualBasic;
using System.Web.Http.ModelBinding;

namespace ZkmBusTimetables.Infrastructure.Repositories
{
    public sealed class VariantsRepository(ZkmDbContext dbContext) : IVariantsRepository
    {
        public async Task<Variant?> GetAsync(
            string lineName,
            Guid variantId,
            CancellationToken cancellationToken)
        {
            var variant = await dbContext.Variants

                .Include(variant => variant.Line)
                .Include(variant => variant.RouteStops.OrderBy(routeStop => routeStop.Order))
                .ThenInclude(routeStop => routeStop.BusStop)
                .Include(variant => variant.RouteLinePoints.OrderBy(routeLinePoint => routeLinePoint.Order))
                .Include(variant => variant.Departures)
                .Where(variant => variant.Id == variantId)
                .AsNoTracking()
                .AsSplitQuery()
                .FirstOrDefaultAsync(cancellationToken);
            /*var variant = await dbContext.Variants
                .AsNoTracking()
                .Include(variant => variant.RouteStops.OrderBy(routeStop => routeStop.Order))
                .ThenInclude(routeStop => routeStop.BusStop)
                .Where(variant => variant.Id == variantId)
                .FirstOrDefaultAsync(cancellationToken);

            var routeLinePoints = await dbContext.RouteLinePoints
                .AsNoTracking()
                .Where(routeLinePoint => routeLinePoint.Variant.Id == variantId)
                .OrderBy(routeLinePoint => routeLinePoint.Order)
                .ToListAsync(cancellationToken);
            foreach (var routeLinePoint in routeLinePoints)
            {
                variant.RouteLinePoints.Add(routeLinePoint);
            }

            var departures = await dbContext.Departures
                .AsNoTracking()
                .Where(departure => departure.Variant.Id == variantId)
                .ToListAsync(cancellationToken);
            foreach (var departure in departures)
            {
                variant.Departures.Add(departure);
            }*/


            return variant;
        }

        public async Task<IEnumerable<Variant>> GetByBusStopIdAsync(string lineName, int busStopId, CancellationToken cancellationToken)
        {
            var variants = await dbContext.Variants
                .Include(variant => variant.RouteStops)
                .Include(variant => variant.Line)
                .Where(variant => variant.Line.Name == lineName && variant.RouteStops.Any(routeStop => routeStop.BusStopId == busStopId))
                .ToListAsync(cancellationToken);

            return variants;
        }

        public async Task<IEnumerable<Variant>> GetAllAsync(string lineName, CancellationToken cancellationToken)
        {
            var lineId = await dbContext.Lines
                .Where(line => line.Name == lineName)
                .Select(line => line.Id)
                .FirstOrDefaultAsync(cancellationToken);

            var variants = await dbContext.Variants
                .Include(variant => variant.RouteLinePoints.OrderBy(routeLinePoints => routeLinePoints.Order))
                .Where(variant => variant.LineId == lineId)
                .ToListAsync(cancellationToken);

            return variants;
        }

        public async Task<Variant> InsertAsync(
            string lineName,
            Variant variant,
            CancellationToken cancellationToken)
        {
            var line = await dbContext.Lines
                .Where(line => line.Name == lineName)
                .FirstOrDefaultAsync(cancellationToken);

            variant.IsDefault = false;
            variant.LineId = line.Id;

            await dbContext.Variants.AddAsync(variant, cancellationToken);

            return await dbContext.SaveChangesAsync(cancellationToken) > 0
                ? variant : throw new HttpResponseException(HttpStatusCode.BadRequest);
        }

        public async Task<bool> UpdateAsync(string lineName, Guid variantId, Variant variantToUpdate, CancellationToken cancellationToken)
        {
            var variant = await dbContext.Variants
                .FindAsync(variantId, cancellationToken)
                ?? throw new HttpResponseException(HttpStatusCode.NotFound);

           dbContext.Variants.Remove(variant);

            await dbContext.SaveChangesAsync(cancellationToken);

            if (await InsertAsync(lineName, variantToUpdate, cancellationToken) is not null)
            {
                return true;
            }
            else return false;
        }

        public async Task<bool> DeleteAsync(Guid variantId, CancellationToken cancellationToken)
        {
            var variant = await dbContext.Variants
                .FindAsync(variantId, cancellationToken);

            dbContext.Variants.Remove(variant);

            return await dbContext.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> DeleteAllAsync(CancellationToken cancellationToken)
        {
            var allVariants = await dbContext.Variants
                .ToListAsync(cancellationToken);

            dbContext.Variants.RemoveRange(allVariants);

            return await dbContext.SaveChangesAsync(cancellationToken) > 0;
        }
    }
}