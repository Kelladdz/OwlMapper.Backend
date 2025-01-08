using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ZkmBusTimetables.Core.Models
{
    public class Line
    {
        public Guid Id { get; private init; } = Guid.NewGuid();
        public string Name { get; set; } = default!;
        [JsonIgnore]
        public ICollection<Variant> Variants { get; init; } = new List<Variant>();
    }
}

