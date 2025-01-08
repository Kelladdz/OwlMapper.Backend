using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ZkmBusTimetables.Application.Abstractions.Messaging;
using ZkmBusTimetables.Application.DTOs.Requests;
using ZkmBusTimetables.Application.DTOs.Responses;
using ZkmBusTimetables.Core.Models;

namespace ZkmBusTimetables.Application.Features.Variants.Insert
{
    public record InsertCommand(string LineName, VariantRequest Request, CurrentUser CurrentUser) : ICommand<VariantResponse>;
}
