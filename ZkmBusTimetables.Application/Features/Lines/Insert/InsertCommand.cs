using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZkmBusTimetables.Application.Abstractions.Messaging;
using ZkmBusTimetables.Application.DTOs.Requests;
using ZkmBusTimetables.Application.DTOs.Responses;

namespace ZkmBusTimetables.Application.Features.Lines.Insert
{
    public record InsertCommand(LineRequest Request) : ICommand<LineResponse>;
}
