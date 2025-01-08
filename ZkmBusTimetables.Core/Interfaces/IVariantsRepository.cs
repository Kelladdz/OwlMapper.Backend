using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZkmBusTimetables.Core.Models;

namespace ZkmBusTimetables.Core.Interfaces
{
    public interface IVariantsRepository
    {
        Task<Variant> GetAsync(string lineName, Guid variantId, CancellationToken cancellationToken);
        Task<IEnumerable<Variant>> GetByBusStopIdAsync(string lineName, int busStopId, CancellationToken cancellationToken);
        Task<IEnumerable<Variant>> GetAllAsync(string lineName, CancellationToken cancellationToken);
        Task<Variant> InsertAsync(string lineName, Variant variant, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(string lineName, Guid variantId, Variant variant, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
        Task<bool> DeleteAllAsync(CancellationToken cancellationToken);
    }
}
