using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ZkmBusTimetables.Core.Models;

namespace ZkmBusTimetables.Infrastructure.Utils.JwtTokenHandler
{
    public interface IJwtTokenHandler
    {
        string GenerateToken(ClaimsIdentity claimsIdentity, DateTime expiry);
        string GenerateAccessToken(UserSession userSession, out string userFingerprint);
        string GenerateRefreshToken(UserSession userSession);
        string GenerateUserFingerprint();
        string GenerateUserFingerprintHash(string userFingerprint);
        ClaimsPrincipal ValidateAndGetPrincipalFromToken(string accessToken);
        IEnumerable<Claim> GetClaimsFromToken(string token);
    }
}