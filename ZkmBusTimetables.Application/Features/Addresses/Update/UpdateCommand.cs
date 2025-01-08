using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZkmBusTimetables.Application.Abstractions.Messaging;

namespace ZkmBusTimetables.Application.Features.Addresses.Update
{
    public record UpdateCommand(Guid Id, JsonPatchDocument AddressesDocument) : ICommand<bool>;
}
