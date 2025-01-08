using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZkmBusTimetables.Application.DTOs.Responses;
using ZkmBusTimetables.Core.Interfaces;

namespace ZkmBusTimetables.Application.Features.Addresses.Search
{
    internal sealed class SearchAddressesQueryHandler(IAddressesRepository addressesRepository) : IRequestHandler<SearchQuery, IEnumerable<AddressResponse>>
    {
        public async Task<IEnumerable<AddressResponse>> Handle(SearchQuery query, CancellationToken cancellationToken)
        {
            var term = query.Term;
            var matchedAddresses = await addressesRepository.SearchAsync(term, cancellationToken);

            var response = new List<AddressResponse>();
            foreach (var address in matchedAddresses)
            {
                response.Add(new AddressResponse(address));
            }

            return response;
        }

    }
}
