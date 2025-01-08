
using Castle.Core.Resource;
using EFCore.BulkExtensions;
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
    public sealed class AddressesRepository(ZkmDbContext dbContext) : IAddressesRepository
    {
        public async Task<Address> GetAsync(Guid id, CancellationToken cancellationToken)
            => await dbContext.Addresses.FindAsync(id, cancellationToken);

        public async Task<IEnumerable<Address>> GetAllAsync(CancellationToken cancellationToken)
            => await dbContext.Addresses.ToListAsync(cancellationToken);
    

        public async Task<IEnumerable<Address>> SearchAsync(string term, CancellationToken cancellationToken)
        {
            /*            List<object> matchingAddresses = new List<object>();

                        if (string.IsNullOrEmpty(term)) return matchingAddresses;

                        matchingAddresses.AddRange(await dbContext.Addresses.Where(x => x.AddressString.Contains(term)).Select(x => new { x.Street, x.City }).Distinct().ToListAsync());

                        if (await dbContext.Addresses.AnyAsync(x => term.Contains(x.AddressString)))
                            matchingAddresses.AddRange(await dbContext.Addresses.Where(x => x.AddressString.Contains(term)).Select(x => new { x.AddressString, x.City }).ToListAsync());

                        return matchingAddresses;*/
            if (string.IsNullOrEmpty(term)) return Enumerable.Empty<Address>();

            var matchingAddresses = await dbContext.Addresses
                .Where(x => x.Street == null ? x.AddressString.Contains(term.ToLower()) : (x.City + ", " + x.AddressString).Contains(term.ToLower()))
                .Distinct()
                .ToListAsync(cancellationToken);

            return matchingAddresses;
        }

        public async Task<IEnumerable<Address>> InsertAsync(IEnumerable<Address> addresses, CancellationToken cancellationToken)
        {
            var bulkConfig = new BulkConfig { SetOutputIdentity = true };
            await dbContext.BulkInsertAsync(addresses, bulkConfig);

            await dbContext.BulkSaveChangesAsync();

            return addresses;
        }

        public async Task<bool> UpdateAsync(Guid id, JsonPatchDocument addressJsonDocument, CancellationToken cancellationToken)
        {
            var address = await dbContext.Addresses
                .FindAsync(id, cancellationToken);

            addressJsonDocument.ApplyTo(address);

            return await dbContext.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var address = await dbContext.Addresses
                .FindAsync(id, cancellationToken);

            dbContext.Addresses.Remove(address);

            return await dbContext.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> DeleteAllAsync(CancellationToken cancellationToken)
        {
            var allAddresses = await dbContext.Addresses
                .ToListAsync(cancellationToken);

            dbContext.Addresses.RemoveRange(allAddresses);

            return await dbContext.SaveChangesAsync(cancellationToken) > 0;
        }
    }
}
