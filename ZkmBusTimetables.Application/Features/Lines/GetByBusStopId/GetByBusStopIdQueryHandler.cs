using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZkmBusTimetables.Core.Interfaces;
using ZkmBusTimetables.Application.DTOs.Responses;
using MediatR;

namespace ZkmBusTimetables.Application.Features.Lines.GetByRouteStopId
{
    internal sealed class GetByBusStopIdQueryHandler(ILinesRepository linesRepository) : IRequestHandler<GetByBusStopIdQuery, IEnumerable<LineResponse>>
    {
        public async Task<IEnumerable<LineResponse>> Handle(GetByBusStopIdQuery query, CancellationToken cancellationToken)
        {
            var busStopId = query.BusStopId;

            var lines = await linesRepository.GetByBusStopIdAsync(busStopId, cancellationToken);

            var response = new List<LineResponse>();
            foreach (var line in lines)
            {
                response.Add(new LineResponse(line));
            }

            return response;
        }
    }
}
