using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text.Json;
using ZkmBusTimetables.Application.DTOs.Responses;
using ZkmBusTimetables.Core.Models;
using ZkmBusTimetables.Infrastructure.Utils.JwtTokenHandler;
using Microsoft.AspNetCore.Http;
using System.Reflection.Metadata.Ecma335;

namespace ZkmBusTimetables.Application.Features.Auth.Login
{
    internal sealed class LoginCommandHandler(UserManager<ApplicationUser> userManager, IJwtTokenHandler jwtTokenHandler) : IRequestHandler<LoginCommand, LoginResponse>
    {
        public async Task<LoginResponse> Handle(LoginCommand command, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByNameAsync(command.Request.UserName);
            if (user is null) 
                return new LoginResponse(false, null, null, null, new List<string> { "Invalid email or password" });

            var checkPasswordResult = await userManager.CheckPasswordAsync(user, command.Request.Password);
            if (!checkPasswordResult)
                return new LoginResponse(false, null, null, null, new List<string> { "Invalid email or password" });

            var getUserRole = await userManager.GetRolesAsync(user);
            if (getUserRole.Count == 0)
            {
                var result = await userManager.AddToRoleAsync(user, "Employee");
                if (!result.Succeeded)
                    return new LoginResponse(false, null, null, null, new List<string> { "Cannot add user to role" });
            }

            var userSession = new UserSession(user.Id, user.UserName, user.FirstName, user.LastName, user.Email, getUserRole[0]);

            var accessToken = jwtTokenHandler.GenerateAccessToken(userSession, out string userFingerprint);
            if (accessToken is null)
                return new LoginResponse(false, null, null, null, new List<string> { "Access token cannot be null" });

            var refreshToken = jwtTokenHandler.GenerateRefreshToken();
            if (refreshToken is null)
                return new LoginResponse(false, null, null, null, new List<string> { "Refresh token cannot be null" });

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpirationDate = DateTime.UtcNow.AddDays(7);

            var userUpdateResult = await userManager.UpdateAsync(user);
            if (!userUpdateResult.Succeeded)
                return new LoginResponse(false, null, null, null, new List<string> { "User update failed." });

            return new LoginResponse(true, accessToken, refreshToken, userFingerprint, null);
        }
    }
}
