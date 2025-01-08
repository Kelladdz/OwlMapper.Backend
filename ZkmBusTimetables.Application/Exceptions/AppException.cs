using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ZkmBusTimetables.Application.Exceptions
{
    public class AppException(HttpStatusCode code, object? errors = null) : Exception
    {
        public HttpStatusCode Code { get; } = code;
        public object? Errors { get; } = errors;
    }
}
