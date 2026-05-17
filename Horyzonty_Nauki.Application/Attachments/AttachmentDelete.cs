using Horyzonty_Nauki.Infrastructure.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horyzonty_Nauki.Application.Attachments
{
    public class AttachmentDelete
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Guid Id { get; set; }
        }
        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var attachment= await _context.Attachments.FindAsync(request.Id);
                if (attachment== null)
                {
                    return Result<Unit>.Failure("Attachment not found");
                }
                _context.Attachments.Remove(attachment);
                var success = await _context.SaveChangesAsync(cancellationToken) > 0;
                if (!success)
                {
                    return Result<Unit>.Failure("Failed to delete the attachment");
                }
                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
