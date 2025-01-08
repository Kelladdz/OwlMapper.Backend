using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZkmBusTimetables.Application.DTOs.Responses;

namespace ZkmBusTimetables.Application.Features.Variants.Get
{
    public record GetQuery(Guid Id, string LineName) : IRequest<VariantResponse>;
}
