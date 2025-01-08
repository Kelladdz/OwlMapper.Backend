using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZkmBusTimetables.Application.DTOs.Responses;
using ZkmBusTimetables.Core.Interfaces;

namespace ZkmBusTimetables.Application.Features.BusStops.GetAll
{
    internal sealed class GetAllQueryHandler(IBusStopsRepository busStopsRepository) : IRequestHandler<GetAllQuery, IEnumerable<BusStopResponse>>
    {
        public async Task<IEnumerable<BusStopResponse>> Handle(GetAllQuery query, CancellationToken cancellationToken)
        {
            var busStops = await busStopsRepository.GetAllAsync(cancellationToken);

            var response = new List<BusStopResponse>();
            foreach (var stop in busStops)
            {
                response.Add(new BusStopResponse(stop));
            }

            return response;
        }
    }
}
