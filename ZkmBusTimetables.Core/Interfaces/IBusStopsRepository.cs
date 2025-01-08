using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZkmBusTimetables.Core.Models;

namespace ZkmBusTimetables.Core.Interfaces
{
    public interface IBusStopsRepository
    {
        Task<BusStop> GetAsync(string slug, CancellationToken cancellationToken);
        Task<IEnumerable<BusStop>> GetAllAsync(CancellationToken cancellationToken);
        Task<IEnumerable<BusStop>> SearchAsync(string term, CancellationToken cancellationToken);
        Task<BusStop> InsertAsync(BusStop busStop, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(string slug, BusStop busStop, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(string slug, CancellationToken cancellationToken);
        Task<bool> DeleteAllAsync(CancellationToken cancellationToken);
        /*        Task<Coordinate> GetBusStopsCoordsAsync(string name, CancellationToken cancellationToken);
                Task<List<Line>> GetBusStopRoutesAsync(string slug, CancellationToken cancellationToken);*/
    }
}
