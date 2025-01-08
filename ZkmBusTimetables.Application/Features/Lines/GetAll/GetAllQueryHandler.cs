using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZkmBusTimetables.Core.Interfaces;
using ZkmBusTimetables.Application.DTOs.Responses;
using ZkmBusTimetables.Infrastructure.Repositories;

namespace ZkmBusTimetables.Application.Features.Lines.GetAll
{
    internal sealed class GetAllQueryHandler(ILinesRepository linesRepository) : IRequestHandler<GetAllQuery, IEnumerable<LineResponse>>
    {
        public async Task<IEnumerable<LineResponse>> Handle(GetAllQuery query, CancellationToken cancellationToken)
        {
            var lines = await linesRepository.GetAllAsync(cancellationToken);

            var response = new List<LineResponse>();
            foreach (var line in lines)
            {
                response.Add(new LineResponse(line));
            }

            return response;
        }
    }
}
