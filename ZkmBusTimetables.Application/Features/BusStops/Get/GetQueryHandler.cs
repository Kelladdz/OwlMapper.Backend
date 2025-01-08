using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZkmBusTimetables.Application.DTOs.Responses;
using ZkmBusTimetables.Core.Interfaces;

namespace ZkmBusTimetables.Application.Features.BusStops.Get
{
    internal sealed class GetQueryHandler(IBusStopsRepository busStopsRepository) : IRequestHandler<GetQuery, BusStopResponse>
    {
        public async Task<BusStopResponse> Handle(GetQuery query, CancellationToken cancellationToken)
        {
            var slug = query.Slug;

            var busStop = await busStopsRepository.GetAsync(slug, cancellationToken);
            return new BusStopResponse(busStop);
        }
    }
}
