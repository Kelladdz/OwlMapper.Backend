using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZkmBusTimetables.Core.Enums;
using ZkmBusTimetables.Core.Models;

namespace ZkmBusTimetables.Core.Interfaces
{
    public interface ILinesRepository
    {
        Task<Line> GetAsync(string name, CancellationToken cancellationToken);
        Task<IEnumerable<Line>> GetByBusStopIdAsync(int busStopId, CancellationToken cancellationToken);
        Task<IEnumerable<Line>> GetAllAsync(CancellationToken cancellationToken);
        Task<Line> InsertAsync(Line line, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(string name, JsonPatchDocument busStopDocument, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(string name, CancellationToken cancellationToken);
        Task<bool> DeleteAllAsync(CancellationToken cancellationToken);
    }
}
