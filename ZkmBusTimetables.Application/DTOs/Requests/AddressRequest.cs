using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZkmBusTimetables.Application.DTOs.Requests
{
    public class AddressRequest
    {
        public string City { get; init; } = default!;
        public string? Street { get; init; }
        public string Number { get; init; } = default!;
    }
}
