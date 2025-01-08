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
        public string GenerateAccessToken(UserSession userSession, out string userFingerprint);
        public string GenerateRefreshToken();
        public string GenerateUserFingerprint();
        public string GenerateUserFingerprintHash(string userFingerprint);
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        public ClaimsPrincipal GetClaims(string token);
    }
}
