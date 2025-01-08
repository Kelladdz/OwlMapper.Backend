using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZkmBusTimetables.Application.DTOs.Responses;
using ZkmBusTimetables.Core.Interfaces;
using ZkmBusTimetables.Infrastructure.Repositories;
using ZkmBusTimetables.Core.Models;

namespace ZkmBusTimetables.Application.Features.Addresses.Insert
{
    internal sealed class InsertCommandHandler(IAddressesRepository addressesRepository, IMapper mapper) : IRequestHandler<InsertCommand, IEnumerable<AddressResponse>>
    {
        public async Task<IEnumerable<AddressResponse>> Handle(InsertCommand command, CancellationToken cancellationToken)
        {
            var addressesToInsert = new List<Address>();
            foreach (var request in command.Request)
            {
                if (request == null) continue;
                var addressToInsert = mapper.Map<Address>(request);
                addressToInsert.Id = Guid.NewGuid();
                addressToInsert.AddressString = addressToInsert.ToString();
                addressesToInsert.Add(addressToInsert);
            }
            var addedAddresses = await addressesRepository.InsertAsync(addressesToInsert, cancellationToken);

            var response = new List<AddressResponse>();
            foreach (var address in addedAddresses)
            {
                response.Add(new AddressResponse(address));
            }

            return response;
        }
    }
}
