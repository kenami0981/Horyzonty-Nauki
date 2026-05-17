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
    public class AttachmentsDetails
    {
        public class Query : IRequest<Result<AttachmentDto>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<AttachmentDto>>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<Result<AttachmentDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var articleDto = await _context.Attachments
                    .Where(b => b.Id == request.Id)
                    .Select(b => new AttachmentDto
                    {
                        Id = b.Id,
                        Id_Article = b.Id_Article,
                        File_name = b.File_name,
                        File_path = b.File_path,
                        File_size = b.File_size,
                        File_type = b.File_type

                    })
                    .FirstOrDefaultAsync(cancellationToken);

                if (articleDto == null)
                {
                    return Result<AttachmentDto>.Failure("Attachment not found");
                }

                return Result<AttachmentDto>.Success(articleDto);
            }
        }
    }
}
