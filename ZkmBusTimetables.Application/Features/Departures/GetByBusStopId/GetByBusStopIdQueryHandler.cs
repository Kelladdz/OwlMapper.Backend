using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ZkmBusTimetables.Application.Utils;
using ZkmBusTimetables.Core.Interfaces;
using ZkmBusTimetables.Core.Models;
using ZkmBusTimetables.Core.Enums;
using ZkmBusTimetables.Application.DTOs.Responses;

namespace ZkmBusTimetables.Application.Features.Departures.GetByBusStopId
{
    internal sealed class GetByBusStopIdQueryHandler(IDeparturesRepository departuresRepository) : IRequestHandler<GetByBusStopIdQuery, IEnumerable<DepartureResponse>>
    {
        public async Task<IEnumerable<DepartureResponse>> Handle(GetByBusStopIdQuery query, CancellationToken cancellationToken)
        {
            var busStopId = query.BusStopId;
            var page = query.Page;
            var pageSize = query.PageSize;
            var lineName = query.LineName;
            var date = query.Date;  

            var scheduleDayValuesForDateAndTwoNextDays = ScheduleDayCalculator.GetScheduleDayValues(date);
            
            var departures = await departuresRepository.GetByBusStopIdAsync(busStopId, lineName, scheduleDayValuesForDateAndTwoNextDays, page, pageSize, cancellationToken);
            var response = new List<DepartureResponse>();

            foreach (var departure in departures)
            {
                response.Add(new DepartureResponse(departure));
            }
            return response;
        }
    }
}
