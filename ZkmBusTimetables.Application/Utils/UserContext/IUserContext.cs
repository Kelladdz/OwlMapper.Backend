using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZkmBusTimetables.Core.Models;

namespace ZkmBusTimetables.Application.Utils.UserContext
{
    public interface IUserContext
    {
        CurrentUser? GetCurrentUser();
    }
}
