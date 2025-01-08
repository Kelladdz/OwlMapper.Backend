using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZkmBusTimetables.Application.DTOs.Responses;
using ZkmBusTimetables.Core.Interfaces;

namespace ZkmBusTimetables.Application.Features.Addresses.Get
{
    internal sealed class GetQueryHandler(IAddressesRepository addressesRepository) : IRequestHandler<GetQuery, AddressResponse>
    {
        public async Task<AddressResponse> Handle(GetQuery query, CancellationToken cancellationToken)
        {
            var id = query.Id;

            var address = await addressesRepository.GetAsync(id, cancellationToken);

            return new AddressResponse(address);
        }

    }
}
