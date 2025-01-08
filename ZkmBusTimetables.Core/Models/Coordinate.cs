using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZkmBusTimetables.Core.Models
{
    [Owned]
    public record Coordinate(double Lat, double Lng);
}
