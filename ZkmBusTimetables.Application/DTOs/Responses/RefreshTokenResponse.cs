using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZkmBusTimetables.Application.DTOs.Responses
{
    public record RefreshTokenResponse(bool Success, string AccessToken, string UserFingerprint, string RefreshToken, List<string>? Errors);
}
