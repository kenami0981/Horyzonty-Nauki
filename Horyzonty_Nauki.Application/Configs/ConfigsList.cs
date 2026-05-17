using Horyzonty_Nauki.Application.Attachments;
using Horyzonty_Nauki.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horyzonty_Nauki.Application.Configs
{
    public class ConfigsList
    {
        public class Query : IRequest<Result<List<ConfigDto>>> { }

        public class Handler : IRequestHandler<Query, Result<List<ConfigDto>>>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<Result<List<ConfigDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var result = await _context.Configs
                    .Select(b => new ConfigDto
                    {
                        Id = b.Id,
                        Issn_number = b.Issn_number,
                        Logo_path = b.Logo_path,
                    })
                    .ToListAsync(cancellationToken);

                return Result<List<ConfigDto>>.Success(result);
            }
        }
    }
}
