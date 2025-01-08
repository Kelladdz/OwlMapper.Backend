using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ZkmBusTimetables.Core.Models
{
    public class RouteStop
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public int BusStopId { get; set; }
        [JsonIgnore]
        public BusStop BusStop { get; set; }
        public Guid VariantId { get; set; }
        [JsonIgnore]
        public Variant Variant { get; set; }
        public int TimeToTravelInMinutes { get; set; }
        public int Order { get; set; }
    }
}
