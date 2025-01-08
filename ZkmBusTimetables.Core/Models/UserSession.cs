using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZkmBusTimetables.Core.Models
{
    public record UserSession(Guid? Id, string? UserName, string? FirstName, string? LastName, string? Email, string? Role)
    {
        public override string ToString() => $"Id: {Id}, User Name: {UserName}, First Name: {FirstName}, Last Name: {LastName}, Email: {Email}, Role: {Role}";
    }
}
