using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZkmBusTimetables.Core.Models;

namespace ZkmBusTimetables.Core.Interfaces
{
    public interface IAddressesRepository
    {
        Task<Address> GetAsync(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<Address>> GetAllAsync(CancellationToken cancellationToken);
        Task<IEnumerable<Address>> SearchAsync(string term, CancellationToken cancellationToken);
        Task<IEnumerable<Address>> InsertAsync(IEnumerable<Address> addresses, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(Guid id, JsonPatchDocument addressesDocument, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
        Task<bool> DeleteAllAsync(CancellationToken cancellationToken);
    }
}
