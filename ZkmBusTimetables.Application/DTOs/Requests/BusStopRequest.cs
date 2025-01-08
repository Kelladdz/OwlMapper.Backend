using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZkmBusTimetables.Core.Models;

namespace ZkmBusTimetables.Application.DTOs.Requests
{
    public record BusStopRequest(
        string Name,
        string City,
        Coordinate Coordinate,
        bool IsRequest);
}
