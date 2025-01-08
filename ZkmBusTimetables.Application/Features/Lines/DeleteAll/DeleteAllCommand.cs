using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZkmBusTimetables.Application.Features.Lines.DeleteAll
{
    public record DeleteAllCommand : IRequest<bool>;
}
