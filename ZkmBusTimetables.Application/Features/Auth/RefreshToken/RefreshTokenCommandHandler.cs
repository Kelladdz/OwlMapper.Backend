﻿using Azure.Core;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ZkmBusTimetables.Infrastructure.Utils.JwtTokenHandler;
using MediatR;
using ZkmBusTimetables.Application.DTOs.Responses;
using Microsoft.AspNetCore.Identity;
using ZkmBusTimetables.Core.Models;
using Azure;
using Microsoft.AspNetCore.Http;
using ZkmBusTimetables.Application.Exceptions;

namespace ZkmBusTimetables.Application.Features.Auth.RefreshToken
{
    internal sealed class RefreshTokenCommandHandler
        (UserManager<ApplicationUser> userManager,
        IJwtTokenHandler tokenHandler,
        IHttpContextAccessor httpContextAccessor) : IRequestHandler<RefreshTokenCommand, LoginResponse>
    {
        public async Task<LoginResponse> Handle(RefreshTokenCommand command, CancellationToken cancellationToken)
        {
            var accessToken = httpContextAccessor.HttpContext!.Request.Headers.Authorization.ToString().Replace("Bearer ", "");
            var refreshToken = command.RefreshToken;

            var accessTokenPrincipal = tokenHandler.ValidateAndGetPrincipalFromToken(accessToken)
                ?? throw new AppException(System.Net.HttpStatusCode.Unauthorized, "Invalid access token");
            var refreshTokenPrincipal = tokenHandler.ValidateAndGetPrincipalFromToken(refreshToken)
                ?? throw new AppException(System.Net.HttpStatusCode.Unauthorized, "Invalid refresh token");

            var accessTokenPrincipalUserId = accessTokenPrincipal.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value
                ?? throw new AppException(System.Net.HttpStatusCode.Unauthorized, "Invalid access token");
            var user = await userManager.FindByIdAsync(accessTokenPrincipalUserId)
                ?? throw new AppException(System.Net.HttpStatusCode.Unauthorized, "User not found");

            var refreshTokenPrincipalUserId = refreshTokenPrincipal.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value
                ?? throw new AppException(System.Net.HttpStatusCode.Unauthorized, "Invalid refresh token");
            var refreshTokenExpiry = refreshTokenPrincipal.Claims.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Exp)?.Value;

            if (accessTokenPrincipalUserId is null || refreshTokenPrincipal is null
                || accessTokenPrincipalUserId != refreshTokenPrincipalUserId)
                throw new AppException(System.Net.HttpStatusCode.Unauthorized, "Invalid refresh token");

            var userRoles = await userManager.GetRolesAsync(user);
            if (userRoles.Count == 0)
            {
                var result = await userManager.AddToRoleAsync(user, "User");
                if (!result.Succeeded) throw new InvalidOperationException("User role cannot be added.");
            }
            var userSession = new UserSession(user.Id, user.UserName, user.Email, userRoles);

            var newAccessToken = tokenHandler.GenerateAccessToken(userSession, out string userFingerprint)
                ?? throw new AppException(System.Net.HttpStatusCode.Unauthorized, "New access token cannot be null.");

            var newRefreshToken = tokenHandler.GenerateRefreshToken(userSession)
                ?? throw new AppException(System.Net.HttpStatusCode.Unauthorized, "New refresh token cannot be null.");

            return new LoginResponse(newAccessToken, newRefreshToken, userFingerprint);
        }
    }
}