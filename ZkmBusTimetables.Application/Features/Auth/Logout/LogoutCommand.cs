using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ZkmBusTimetables.Application.DTOs.Responses;

namespace ZkmBusTimetables.Application.Features.Auth.Logout
{
    public record LogoutCommand(string AccessToken) : IRequest<LogoutResponse>;
}
