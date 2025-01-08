using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZkmBusTimetables.Application.DTOs.Responses;
using ZkmBusTimetables.Core.Models;

namespace ZkmBusTimetables.Application.Features.Auth.Register
{
    public class RegisterCommandHandler(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IMapper mapper) : IRequestHandler<RegisterCommand, RegisterResponse>
    {
        public async Task<RegisterResponse> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            var request = command.Request;
            var user = mapper.Map<ApplicationUser>(request);

            var createUserResult = await userManager.CreateAsync(user, request.Password!);
            if (!createUserResult.Succeeded)
                return new RegisterResponse(false, null, createUserResult.Errors.Select(e => e.Description).ToList());

            if (!await roleManager.RoleExistsAsync("Employee"))
            {
                var role = new ApplicationRole { Id = Guid.NewGuid(), Name = "Employee", NormalizedName = "EMPLOYEE", ConcurrencyStamp = Guid.NewGuid().ToString() };
                var createRoleResult = await roleManager.CreateAsync(role);

                if (!createRoleResult.Succeeded)
                    return new RegisterResponse(false, null, createRoleResult.Errors.Select(e => e.Description).ToList());
            }

            var addToRoleResult = await userManager.AddToRoleAsync(user, "Employee");
            if (!addToRoleResult.Succeeded)
                return new RegisterResponse(false, null, addToRoleResult.Errors.Select(e => e.Description).ToList());

            return new RegisterResponse(true, user, null);
        }
    }
}
