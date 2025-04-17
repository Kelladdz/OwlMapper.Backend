using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ZkmBusTimetables.Core.Models;

namespace ZkmBusTimetables.Application.DTOs.Responses
{
    public record RegisterResponse(ApplicationUser? User);

}