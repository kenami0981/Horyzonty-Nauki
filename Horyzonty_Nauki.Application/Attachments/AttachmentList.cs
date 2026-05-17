using Horyzonty_Nauki.Application.Articles;
using Horyzonty_Nauki.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horyzonty_Nauki.Application.Attachments
{
    public class AttachmentList
    {
        public class Query : IRequest<Result<List<AttachmentDto>>> { }

        public class Handler : IRequestHandler<Query, Result<List<AttachmentDto>>>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<Result<List<AttachmentDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var result = await _context.Attachments
                    .Select(b => new AttachmentDto
                    {
                        Id = b.Id,
                        Id_Article = b.Id_Article,
                        File_name = b.File_name,
                        File_type = b.File_type,
                        File_size = b.File_size,
                        File_path = b.File_path
                    })
                    .ToListAsync(cancellationToken);

                return Result<List<AttachmentDto>>.Success(result);
            }
        }
    }
}
