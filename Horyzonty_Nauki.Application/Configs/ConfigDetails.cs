using Horyzonty_Nauki.Application.Attachments;
using Horyzonty_Nauki.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Horyzonty_Nauki.Application.Configs
{
    public class ConfigDetails
    {
        public class Query : IRequest<Result<ConfigDto>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<ConfigDto>>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<Result<ConfigDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var configDto = await _context.Configs
                    .Where(b => b.Id == request.Id)
                    .Select(b => new ConfigDto
                    {
                        Id = b.Id,
                        Issn_number = b.Issn_number,
                        Logo_path = b.Logo_path,

                    })
                    .FirstOrDefaultAsync(cancellationToken);

                if (configDto == null)
                {
                    return Result<ConfigDto>.Failure("Config not found");
                }

                return Result<ConfigDto>.Success(configDto);
            }
        }
    }
}
