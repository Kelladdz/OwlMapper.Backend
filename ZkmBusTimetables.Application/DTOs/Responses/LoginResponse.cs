using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZkmBusTimetables.Core.Models;

namespace ZkmBusTimetables.Application.DTOs.Responses
{
    public record LoginResponse(string AccessToken, string UserFingerprint, string RefreshToken);
}