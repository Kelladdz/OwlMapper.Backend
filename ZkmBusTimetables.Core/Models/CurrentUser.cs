using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZkmBusTimetables.Core.Models
{
    public class CurrentUser(Guid id, string email, string FirstName, string LastName, string role)
    {

        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

        public bool IsInRole(string role) => Role.Contains(role);
    }
}
