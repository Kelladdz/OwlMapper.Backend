using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ZkmBusTimetables.Core.Models
{
    public class RouteLinePoint
    {
        public Guid Id { get; private init; } = Guid.NewGuid();
        public Guid VariantId { get; set; }
        [JsonIgnore]
        public Variant Variant { get; set; }
        public Coordinate Coordinate { get; init; }
        public bool IsManuallyAdded { get; set; }
        public int Order { get; set; }
    }
}
