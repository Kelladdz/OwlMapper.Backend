using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ZkmBusTimetables.Core.Models;
using ZkmBusTimetables.Infrastructure.Utils.JwtTokenHandler;
using ZkmBusTimetables.Application.DTOs.Responses;

namespace ZkmBusTimetables.Application.Features.Auth.Logout
{
    internal sealed class LogoutCommandHandler(IJwtTokenHandler jwtTokenHandler, UserManager<ApplicationUser> userManager) : IRequestHandler<LogoutCommand, LogoutResponse>
    {
        public async Task<LogoutResponse> Handle(LogoutCommand command, CancellationToken cancellationToken)
        {
            var accessToken = command.AccessToken;
            if (string.IsNullOrWhiteSpace(accessToken))
                return new LogoutResponse(false, new List<string> { "Access token is null or empty" });

            string bearerToken = accessToken.Replace("Bearer ", "", StringComparison.InvariantCultureIgnoreCase);

            var principal = jwtTokenHandler.GetClaims(bearerToken);
            if (principal == null)
                return new LogoutResponse(false, new List<string> { "Cannot get claims from access token" });

            var userId = principal.Claims.FirstOrDefault(claim => claim.Type == "sub")?.Value;
            if (userId is null)
                return new LogoutResponse(false, new List<string> { "Cannot get userId from access token" });

            var user = await userManager.FindByIdAsync(userId);
            if (user is null)
                return new LogoutResponse(false, new List<string> { "Cannot find user with this id" });

            return new LogoutResponse(true, null);
        }
    }    
}
