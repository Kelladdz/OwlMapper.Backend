using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZkmBusTimetables.Core.Models
{
    public sealed record UserSession(Guid Id, string UserName, string Email, IList<string> Roles)
    {
        public override string ToString() => $"Id: {Id}, Name: {UserName}, Email: {Email}, Roles: {Roles}";
    }
}