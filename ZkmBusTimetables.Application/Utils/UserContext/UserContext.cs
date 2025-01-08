using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ZkmBusTimetables.Core.Models;

namespace ZkmBusTimetables.Application.Utils.UserContext
{
    public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
    {
        public CurrentUser? GetCurrentUser()
        {
            var user = httpContextAccessor?.HttpContext?.User
                ?? throw new InvalidOperationException("Context user is not present");

            if (user.Identity == null || !user.Identity.IsAuthenticated)
            {
                return null;
            }


            var id = Guid.Parse(user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value);
            var firstName = user.FindFirst(c => c.Type == "firstName")!.Value;
            var lastName = user.FindFirst(c => c.Type == "lastName")!.Value;
            var email = user.FindFirst(c => c.Type == ClaimTypes.Email)!.Value;
            var role = user.FindFirst(c => c.Type == "role")!.Value;

            return new CurrentUser(id, email, firstName, lastName, role);
        }
    }
}
