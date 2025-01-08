using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZkmBusTimetables.Application.DTOs.Responses;
using ZkmBusTimetables.Core.Interfaces;
using ZkmBusTimetables.Infrastructure.Repositories;

namespace ZkmBusTimetables.Application.Features.BusStops.Search
{
    internal sealed class SearchQueryHandler(IBusStopsRepository busStopsRepository) : IRequestHandler<SearchQuery, IEnumerable<BusStopResponse>>
    {
        public async Task<IEnumerable<BusStopResponse>> Handle(SearchQuery query, CancellationToken cancellationToken)
        {
            var term = query.Term;
            var matchingBusStops = await busStopsRepository.SearchAsync(term, cancellationToken);

            var response = new List<BusStopResponse>();
            foreach (var busStop in matchingBusStops)
            {
                response.Add(new BusStopResponse(busStop));
            }

            return response;
        }

    }
}
