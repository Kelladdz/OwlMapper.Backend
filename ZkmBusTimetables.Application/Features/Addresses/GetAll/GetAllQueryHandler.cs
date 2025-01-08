using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZkmBusTimetables.Application.DTOs.Responses;
using ZkmBusTimetables.Core.Interfaces;
using ZkmBusTimetables.Infrastructure.Repositories;

namespace ZkmBusTimetables.Application.Features.Addresses.GetAll
{
    internal sealed class GetAllQueryHandler(IAddressesRepository addressesRepository) : IRequestHandler<GetAllQuery, IEnumerable<AddressResponse>>
    {
        public async Task<IEnumerable<AddressResponse>> Handle(GetAllQuery query, CancellationToken cancellationToken)
        {
            var addresses = await addressesRepository.GetAllAsync(cancellationToken);

            var response = new List<AddressResponse>();
            foreach (var address in addresses)
            {
                response.Add(new AddressResponse(address));
            }

            return response;
        }
    }
}
