using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ZkmBusTimetables.Infrastructure.Exceptions
{
    public class RestException(HttpStatusCode code, object? errors = null) : Exception
    {
        public HttpStatusCode Code { get; } = code;
        public object? Errors { get; } = errors;
    }
}
