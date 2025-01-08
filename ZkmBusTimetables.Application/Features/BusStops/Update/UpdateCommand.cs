using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZkmBusTimetables.Application.Abstractions.Messaging;
using ZkmBusTimetables.Application.DTOs.Requests;

namespace ZkmBusTimetables.Application.Features.BusStops.Update
{
    public record UpdateCommand(string Slug, BusStopRequest Request) : ICommand<bool>;
}
