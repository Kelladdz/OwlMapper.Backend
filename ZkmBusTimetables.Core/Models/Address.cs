using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZkmBusTimetables.Core.Models
{
    public class Address
    {
        public Guid Id { get; set; }
        public string City { get; set; } = default!;
        public string? Street { get; set; }
        public string Number { get; set; } = default!;
        public string AddressString { get; set; } = default!;

        public override string ToString() => Street == null ? $"{City} {Number}" : $"{Street} {Number}";
    }
}
