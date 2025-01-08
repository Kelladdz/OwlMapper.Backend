using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZkmBusTimetables.Core.Models;
using MediatR;
using ZkmBusTimetables.Application.DTOs.Responses;

namespace ZkmBusTimetables.Application.Features.Departures.GetByBusStopId
{
    public record GetByBusStopIdQuery(int BusStopId, int Page, int PageSize, string LineName, DateOnly Date) : IRequest<IEnumerable<DepartureResponse>>;
}
