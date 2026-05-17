using FluentValidation;
using Horyzonty_Nauki.Application.Articles;
using Horyzonty_Nauki.Infrastructure.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horyzonty_Nauki.Application.Attachments
{
    public class AttachmentsEdit
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Guid Id { get; set; }
            public required AttachmentsCreateDto AttachmentsCreateDto { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.AttachmentsCreateDto).SetValidator(new AttachmentsValidator());
            }
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
                var attachment= await _context.Attachments.FindAsync(new object[] { request.Id }, cancellationToken);

                if (attachment == null)
                {
                    return Result<Unit>.Failure("Attachment not found");
                }
                attachment.Id = request.AttachmentsCreateDto.Id;
                attachment.Id_Article = request.AttachmentsCreateDto.Id_Article;
                attachment.File_name = request.AttachmentsCreateDto.File_name;
                attachment.File_path = request.AttachmentsCreateDto.File_path;
                attachment.File_size = request.AttachmentsCreateDto.File_size;
                attachment.File_type = request.AttachmentsCreateDto.File_type;



                var result = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (!result)
                {
                    return Result<Unit>.Failure("Failed to update attachment (or no changes detected)");
                }

                return Result<Unit>.Success(Unit.Value);
            }
        }

    }
}
