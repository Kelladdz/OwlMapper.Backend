using Azure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZkmBusTimetables.Infrastructure.Utils.JwtTokenHandler;
using MediatR;
using ZkmBusTimetables.Application.DTOs.Responses;
using Microsoft.AspNetCore.Identity;
using ZkmBusTimetables.Core.Models;
using Azure;
using Microsoft.AspNetCore.Http;

namespace ZkmBusTimetables.Application.Features.Auth.RefreshToken
{
    internal sealed class RefreshTokenCommandHandler(IJwtTokenHandler jwtTokenHandler, UserManager<ApplicationUser> userManager) : IRequestHandler<RefreshTokenCommand, RefreshTokenResponse>
    {
        public async Task<RefreshTokenResponse> Handle(RefreshTokenCommand command, CancellationToken cancellationToken)
        {
            var principal = jwtTokenHandler.GetClaims(command.AccessToken);
            if (principal is null)
                return new RefreshTokenResponse(false, null, null, null, new List<string> { "Cannot get claims from access token"});

            var userId = principal.Claims.FirstOrDefault(claim => claim.Type == "sub")?.Value;
            if (userId is null)
                return new RefreshTokenResponse(false, null, null, null, new List<string> { "Cannot get userId from access token" });

            var user = await userManager.FindByIdAsync(userId);
            if (user is null)
                return new RefreshTokenResponse(false, null, null, null, new List<string> { "Cannot find user with this id" });

            if (user.RefreshToken != command.RefreshToken || user.RefreshTokenExpirationDate <= DateTime.UtcNow)
                return new RefreshTokenResponse(false, null, null, null, new List<string> { "Refresh Token is invalid" });

            var getUserRole = await userManager.GetRolesAsync(user);
            if (getUserRole.Count == 0)
            {
                var result = await userManager.AddToRoleAsync(user, "Employee");
                if (!result.Succeeded)
                    return new RefreshTokenResponse(false, null, null, null, new List<string> { "Employee role cannot be added" });
            }
            var userSession = new UserSession(user.Id, user.UserName, user.FirstName, user.LastName, user.Email, getUserRole[0]);

            var newAccessToken = jwtTokenHandler.GenerateAccessToken(userSession, out string userFingerprint);
            if (newAccessToken is null)
                return new RefreshTokenResponse(false, null, null, null, new List<string> { "New access token cannot be null" });

            var newRefreshToken = jwtTokenHandler.GenerateRefreshToken();
            if (newRefreshToken is null)
                return new RefreshTokenResponse(false, null, null, null, new List<string> { "New refresh token cannot be null" });

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpirationDate = DateTime.UtcNow.AddDays(7);
            var userUpdateResult = await userManager.UpdateAsync(user);
            if (!userUpdateResult.Succeeded)
                return new RefreshTokenResponse(false, null, null, null, new List<string> { "User update failed" });
            
            return new RefreshTokenResponse(true, newAccessToken, newRefreshToken, userFingerprint, null);
        }
    }
}
