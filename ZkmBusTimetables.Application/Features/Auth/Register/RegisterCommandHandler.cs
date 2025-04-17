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
using GoodBadHabitsTracker.Application.Exceptions;
using ZkmBusTimetables.Application.DTOs.Responses;
using ZkmBusTimetables.Application.Exceptions;
using ZkmBusTimetables.Core.Models;

namespace ZkmBusTimetables.Application.Features.Auth.Register
{
    internal sealed class RegisterCommandHandler(
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager) : IRequestHandler<RegisterCommand, RegisterResponse>
    {
        public async Task<RegisterResponse> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            ApplicationUser user = new()
            {
                FirstName = command.Request.FirstName,
                LastName = command.Request.LastName,
                Email = command.Request.Email,
                UserName = command.Request.UserName,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var createUserResult = await userManager.CreateAsync(user, command.Request.Password);
            if (!createUserResult.Succeeded)
                throw new ValidationException(createUserResult.Errors.Select(e =>new ValidationError(e.Description.Split(' ')[0], e.Description)));

            var isRoleExists = await roleManager.RoleExistsAsync("User");
            if (!isRoleExists)
            {
                var role = new ApplicationRole() { Id = Guid.NewGuid(), Name = "User", NormalizedName = "USER", ConcurrencyStamp = Guid.NewGuid().ToString() };

                var createRoleResult = await roleManager.CreateAsync(role);
                if (!createRoleResult.Succeeded)
                    throw new AppException(System.Net.HttpStatusCode.BadRequest, "Failed to create role: " + string.Join(", ", createRoleResult.Errors.Select(e => e.Description)));
            }

            var addToRoleResult = await userManager.AddToRoleAsync(user, "User");
            if (!addToRoleResult.Succeeded)
                throw new AppException(System.Net.HttpStatusCode.BadRequest, "Failed to add user to role: " + string.Join(", ", addToRoleResult.Errors.Select(e => e.Description)));

            return new RegisterResponse(user);
        }
    }
}