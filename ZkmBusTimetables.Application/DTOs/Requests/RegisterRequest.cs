using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZkmBusTimetables.Application.DTOs.Requests
{
    public record RegisterRequest(string FirstName, string LastName, string Email, string UserName, string Password, string ConfirmPassword);
}
