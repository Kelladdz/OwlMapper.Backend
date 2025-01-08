using MediatR;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZkmBusTimetables.Application.Abstractions.Messaging;
using ZkmBusTimetables.Application.DTOs.Requests;
using ZkmBusTimetables.Application.DTOs.Responses;
using ZkmBusTimetables.Core.Models;

namespace ZkmBusTimetables.Application.Features.Addresses.Insert
{
    public record InsertCommand(IEnumerable<AddressRequest> Request) : ICommand<IEnumerable<AddressResponse>>;
}
