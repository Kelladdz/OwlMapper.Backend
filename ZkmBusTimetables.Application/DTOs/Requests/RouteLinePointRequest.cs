using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZkmBusTimetables.Core.Models;

namespace ZkmBusTimetables.Application.DTOs.Requests
{
    public record RouteLinePointRequest(Coordinate Coordinate, int Order, bool IsManuallyAdded);
}
