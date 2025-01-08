using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZkmBusTimetables.Application.Features.Variants.Delete
{
    public record DeleteCommand(Guid Id) : IRequest<bool>;
}
