using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZkmBusTimetables.Application.DTOs.Requests
{
    public record RouteStopRequest(int BusStopId, string Name, int TimeToTravelInMinutes, int Order);
}
