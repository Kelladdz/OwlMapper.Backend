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

namespace ZkmBusTimetables.WebApi.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController(IMediator mediator) : ControllerBase
    {
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken cancellationToken)
        {
            var response = await mediator.Send(new RegisterCommand(request), cancellationToken);

            var user = response!.User;
            var userId = response!.User.Id;

            if (response is not null)
            {
                return CreatedAtAction(nameof(Register), new { id = userId }, response);
            }
            else return BadRequest("Something goes wrong");
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
        {
            var response = await mediator.Send(new LoginCommand(request), cancellationToken);

            var userFingerprint = response.UserFingerprint;
            var accessToken = response.AccessToken;
            var refreshToken = response.RefreshToken;

            AppendCookies(userFingerprint, refreshToken);

            return Ok(new {accessToken});
        }

        [HttpPost("logout")]
        [AllowAnonymous]
        public IActionResult Logout()
        {
            DeleteCookies();
            return NoContent();
        }

        [HttpPost("token/refresh")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken
            (CancellationToken cancellationToken)
        {
            var refreshToken = Request.Cookies["__Secure-Fgp"];
            var response = await mediator.Send(new RefreshTokenCommand(refreshToken), cancellationToken);

            var userFingerprint = response.UserFingerprint;
            var accessToken = response.AccessToken;
            var newRefreshToken = response.RefreshToken;

            Response.Cookies.Delete("__Secure-Rt");
            AppendCookies(userFingerprint, newRefreshToken);

            return Ok(accessToken);
        }
        private void AppendCookies(string userFingerprint, string refreshToken)
        {
            Response.Cookies.Append("__Secure-Fgp", userFingerprint, new CookieOptions
            {
                SameSite = SameSiteMode.Strict,
                HttpOnly = true,
                Secure = true,
                MaxAge = TimeSpan.FromMinutes(15),
                Path = "/",
            });
            Response.Cookies.Append("__Secure-Rt", refreshToken, new CookieOptions
            {
                SameSite = SameSiteMode.Strict,
                HttpOnly = false,
                Secure = true,
                MaxAge = TimeSpan.FromDays(30),
                Path = "/",
            });
        }
        private void DeleteCookies()
        {
            if (Request.Cookies.TryGetValue("__Secure-Fgp", out var _ ))
            {
                Response.Cookies.Delete("__Secure-Fgp", new CookieOptions
                {
                    SameSite = SameSiteMode.Strict,
                    HttpOnly = true,
                    Secure = true,
                    MaxAge = TimeSpan.FromMinutes(15),
                    Path = "/",
                    Expires = DateTime.UtcNow.AddDays(-2)
                });
            }
            if (Request.Cookies.TryGetValue("__Secure-Rt", out var _))
            {
                Response.Cookies.Delete("__Secure-Rt", new CookieOptions
                {
                    SameSite = SameSiteMode.Strict,
                    HttpOnly = false,
                    Secure = true,
                    MaxAge = TimeSpan.FromDays(30),
                    Path = "/",
                    Expires = DateTime.UtcNow.AddDays(-2)
                });
            }

            return;
        }
    }
}