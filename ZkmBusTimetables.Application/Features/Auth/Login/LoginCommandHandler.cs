using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

using ZkmBusTimetables.Application.DTOs.Responses;
using ZkmBusTimetables.Core.Models;
using ZkmBusTimetables.Infrastructure.Utils.JwtTokenHandler;
using Microsoft.AspNetCore.Http;
using System.Reflection.Metadata.Ecma335;
using ZkmBusTimetables.Application.Exceptions;

namespace ZkmBusTimetables.Application.Features.Auth.Login
{
    internal sealed class LoginCommandHandler
    (UserManager<ApplicationUser> userManager,
        IJwtTokenHandler tokenHandler) : IRequestHandler<LoginCommand, LoginResponse>
    {
        public async Task<LoginResponse> Handle(LoginCommand command, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByNameAsync(command.Request.UserName)
                       ?? throw new AppException(System.Net.HttpStatusCode.Unauthorized, "Invalid name or password");

            var checkPasswordResult = await userManager.CheckPasswordAsync(user, command.Request.Password);
            if (!checkPasswordResult) throw new AppException(System.Net.HttpStatusCode.Unauthorized, "Invalid name or password");

            var userRoles = await userManager.GetRolesAsync(user);
            if (userRoles.Count == 0)
            {
                var addToRoleResult = await userManager.AddToRoleAsync(user, "User");
                if (!addToRoleResult.Succeeded)
                    throw new AppException(System.Net.HttpStatusCode.BadRequest, "Failed to add user to role: " + string.Join(", ", addToRoleResult.Errors.Select(e => e.Description)));
            }

            var userSession = new UserSession(user.Id, user.UserName!, user.Email, userRoles);

            var accessToken = tokenHandler.GenerateAccessToken(userSession, out string userFingerprint)
                              ?? throw new AppException(System.Net.HttpStatusCode.Unauthorized, "Something goes wrong. Try again.");

            var refreshToken = tokenHandler.GenerateRefreshToken(userSession)
                               ?? throw new AppException(System.Net.HttpStatusCode.Unauthorized, "Something goes wrong. Try again.");

            return new LoginResponse(accessToken, refreshToken, userFingerprint);
        }
    }
}