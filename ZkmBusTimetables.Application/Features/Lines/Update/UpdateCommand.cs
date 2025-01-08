using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZkmBusTimetables.Application.Abstractions.Messaging;

namespace ZkmBusTimetables.Application.Features.Lines.Update
{
    public record UpdateCommand(string Name, JsonPatchDocument LinesDocument) : ICommand<bool>;
}
