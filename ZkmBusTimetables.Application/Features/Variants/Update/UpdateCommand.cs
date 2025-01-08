using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZkmBusTimetables.Application.Abstractions.Messaging;
using ZkmBusTimetables.Application.DTOs.Requests;
using ZkmBusTimetables.Core.Models;

namespace ZkmBusTimetables.Application.Features.Variants.Update
{
    public record UpdateCommand(string LineName, Guid Id, VariantRequest Request) : ICommand<bool>;
}
