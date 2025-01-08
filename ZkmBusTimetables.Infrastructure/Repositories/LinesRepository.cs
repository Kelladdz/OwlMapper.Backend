using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using PublicHoliday;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using ZkmBusTimetables.Core.Enums;
using ZkmBusTimetables.Core.Interfaces;
using ZkmBusTimetables.Core.Models;
using ZkmBusTimetables.Infrastructure.Persistance;

namespace ZkmBusTimetables.Infrastructure.Repositories
{
    public sealed class LinesRepository(ZkmDbContext dbContext) : ILinesRepository
    {
        public async Task<Line> GetAsync(string name, CancellationToken cancellationToken)
        {
            var line = await dbContext.Lines
                .Where(line => line.Name == name)
                .Include(line => line.Variants)
                .ThenInclude(variant => variant.RouteStops)
                .ThenInclude(routeStop => routeStop.BusStop)
                .Include(line => line.Variants)
                .ThenInclude(variant => variant.RouteLinePoints)
                .Include(line => line.Variants)
                .ThenInclude(variant => variant.Departures)
                .FirstOrDefaultAsync(cancellationToken);
            return line;
        }

        public async Task<IEnumerable<Line>> GetByBusStopIdAsync(int busStopId, CancellationToken cancellationToken)
        {
            var lines = await dbContext.Lines
                .Include(line => line.Variants)
                .ThenInclude(variant => variant.RouteStops)
                .Include(line => line.Variants)
                .ThenInclude(variant => variant.Departures)
                .Where(line => line.Variants.Any(variant => variant.RouteStops.Any(routeStop => routeStop.BusStop.Id == busStopId)))
                .OrderBy(line => line.Name)
                .AsNoTracking()
                .AsSplitQuery()
                .ToListAsync(cancellationToken)
                ?? throw new HttpResponseException(HttpStatusCode.NotFound);

            return lines;
        }
        
        public async Task<IEnumerable<Line>> GetAllAsync(CancellationToken cancellationToken)
        {
            var lines = await dbContext.Lines
                .ToListAsync(cancellationToken)
                ?? throw new HttpResponseException(HttpStatusCode.NotFound);

            return lines;
        }

        public async Task<Line> InsertAsync(Line line, CancellationToken cancellationToken)
        {
            await dbContext.Lines.AddAsync(line, cancellationToken);

            return await dbContext.SaveChangesAsync(cancellationToken) > 0
                ? line : throw new HttpResponseException(HttpStatusCode.BadRequest);
        }

        public async Task<bool> UpdateAsync(string name, JsonPatchDocument lineJsonDocument, CancellationToken cancellationToken)
        {
            var line = await dbContext.Lines
                .Where(line => line.Name == name)
                .FirstOrDefaultAsync(cancellationToken)
                ?? throw new HttpResponseException(HttpStatusCode.NotFound);

            lineJsonDocument.ApplyTo(line);

            return await dbContext.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> DeleteAsync(string name, CancellationToken cancellationToken)
        {
            var line = await dbContext.Lines
                .Where(line => line.Name == name)
                .FirstOrDefaultAsync(cancellationToken)
                ?? throw new HttpResponseException(HttpStatusCode.NotFound);

            dbContext.Lines.Remove(line);

            return await dbContext.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> DeleteAllAsync(CancellationToken cancellationToken)
        {
            var allLines = await dbContext.Lines
                .ToListAsync(cancellationToken);

            dbContext.Lines.RemoveRange(allLines);

            return await dbContext.SaveChangesAsync(cancellationToken) > 0;
        }
    }
}
