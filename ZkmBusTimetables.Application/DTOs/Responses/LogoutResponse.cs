using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZkmBusTimetables.Application.DTOs.Responses
{
    public record LogoutResponse(bool Success, List<string> Errors);
}
