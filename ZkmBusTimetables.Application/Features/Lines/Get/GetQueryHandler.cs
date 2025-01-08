using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZkmBusTimetables.Application.DTOs.Responses;
using ZkmBusTimetables.Core.Interfaces;

namespace ZkmBusTimetables.Application.Features.Lines.Get
{
    internal sealed class GetQueryHandler(ILinesRepository linesRepository) : IRequestHandler<GetQuery, LineResponse>
    {
        public async Task<LineResponse> Handle(GetQuery query, CancellationToken cancellationToken)
        {
            var name = query.LineName;

            var line = await linesRepository.GetAsync(name, cancellationToken);
            return new LineResponse(line);
        }
    }
}
