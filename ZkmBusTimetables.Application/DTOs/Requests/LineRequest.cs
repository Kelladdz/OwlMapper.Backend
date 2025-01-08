using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZkmBusTimetables.Application.DTOs.Requests
{
    public record LineRequest
    {
        public required string LineName { get; init; }
        public required DateOnly ValidFrom { get; init; }
        public required string Route { get; init; }
        public List<RouteStopRequest> RouteStops { get; init; }
        public IEnumerable<DepartureRequest> Departures { get; init; }
        public List<RouteLinePointRequest> RouteLinePoints { get; init; }
    }
}
