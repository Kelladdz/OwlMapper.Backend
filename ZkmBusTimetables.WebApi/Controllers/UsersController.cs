using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ZkmBusTimetables.Core.Models;
using Microsoft.AspNetCore.Authorization;

namespace ZkmBusTimetables.WebApi.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController(UserManager<ApplicationUser> userManager) : ControllerBase
    {
        [HttpGet("{userId}", Name = "GetUserById")]
        public async Task<IActionResult> GetUserById(Guid userId)
        {
            var user = await userManager.FindByIdAsync(userId.ToString());
            return Ok(user);
        }
    }
}