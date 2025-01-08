using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZkmBusTimetables.Application.DTOs.Requests
{
    public record VariantRequest
    {
        public required DateOnly ValidFrom { get; init; }
        public required string Route { get; init; }
        public required List<RouteStopRequest> RouteStops { get; init; }
        public required IEnumerable<DepartureRequest> Departures { get; init; }
        public required List<RouteLinePointRequest> RouteLinePoints { get; init; }
    }
}
