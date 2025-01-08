using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZkmBusTimetables.Application.DTOs.Responses;

namespace ZkmBusTimetables.Application.Features.Variants.GetAll
{
    public record GetAllQuery(string LineName) : IRequest<IEnumerable<VariantResponse>>;
}
