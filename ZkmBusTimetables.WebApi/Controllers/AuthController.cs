using Azure.Core;
using MediatR;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ZkmBusTimetables.Application.DTOs.Requests;
using ZkmBusTimetables.Application.Features.Auth.Login;
using ZkmBusTimetables.Application.Features.Auth.Register;
using System.Net;
using ZkmBusTimetables.Core.Models;
using System.Threading;
using ZkmBusTimetables.Application.Features.Auth.RefreshToken;
using ZkmBusTimetables.Application.Features.Auth.Logout;

namespace ZkmBusTimetables.WebApi.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController(IMediator mediator) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken cancellationToken)
        {
            var response = await mediator.Send(new RegisterCommand(request), cancellationToken);
            return response.Success 
                ? CreatedAtRoute("GetUserById", new { userId = response.User?.Id }, response)
                : BadRequest(response);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
        {
            var response = await mediator.Send(new LoginCommand(request), cancellationToken);
            if (response.Success)
            {
                Response.Cookies.Append("__Secure-Fgp", response.UserFingerprint, new CookieOptions
                {
                    SameSite = SameSiteMode.Strict,
                    HttpOnly = false,
                    Secure = true,
                    MaxAge = TimeSpan.FromMinutes(15),
                }); 
                var authType = User.Identity.AuthenticationType;
                return Ok(new { accessToken = response.AccessToken.ToString(), refreshToken = response.RefreshToken, AuthenticationType = authType });
            }
            else return Unauthorized(response); 
        }

        [HttpPost("token/refresh")]
        [Authorize(Policy = "AuthPolicy")]
        public async Task<IActionResult> RefreshToken([FromHeader(Name = "Authorization")] string accessToken, [FromBody] string refreshToken, CancellationToken cancellationToken)
        {
            var response = await mediator.Send(new RefreshTokenCommand(accessToken, refreshToken), cancellationToken);
            if (response.Success)
            {
                Response.Cookies.Append("__Secure-Fgp", response.UserFingerprint, new CookieOptions
                {
                    SameSite = SameSiteMode.Strict,
                    HttpOnly = false,
                    Secure = true,
                    MaxAge = TimeSpan.FromMinutes(15),
                });
                var authType = User.Identity.AuthenticationType;
                return Ok(new { accessToken = response.AccessToken.ToString(), refreshToken = response.RefreshToken, AuthenticationType = authType });
            }
            else return Unauthorized(response);
        }

        [HttpPost("logout")]
        [Authorize(Policy = "AuthPolicy")]
        public async Task<IActionResult> Logout([FromHeader(Name = "Authorization")] string accessToken, CancellationToken cancellationToken)
        {
            var response = await mediator.Send(new LogoutCommand(accessToken), cancellationToken);
            if (response.Success)
            {
                Response.Cookies.Delete("__Secure-Fgp", new CookieOptions
                {
                    SameSite = SameSiteMode.Strict,
                    HttpOnly = false,
                    Secure = true,
                    MaxAge = TimeSpan.FromMinutes(15),
                });
                return Ok();
            }
            else return Unauthorized(response);
        }
    }
}
