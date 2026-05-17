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
    public class AttachmentCreate
    {
        public class Command : IRequest<Result<AttachmentDto>>
        {
            public required AttachmentCreateDto AttachmentsCreateDto { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.AttachmentsCreateDto).SetValidator(new AttachmentValidator());
            }
        }

        public class Handler : IRequestHandler<Command, Result<AttachmentDto>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Result<AttachmentDto>> Handle(Command request, CancellationToken cancellationToken)
            {


                var attachment= new Horyzonty_Nauki.Domain.Attachment
                {
                    Id = Guid.NewGuid(),
                    Id_Article = request.AttachmentsCreateDto.Id_Article,
                    File_name = request.AttachmentsCreateDto.File_name,
                    File_path = request.AttachmentsCreateDto.File_path,
                    File_size = request.AttachmentsCreateDto.File_size,
                    File_type = request.AttachmentsCreateDto.File_type
                    
                };

                _context.Attachments.Add(attachment);
                var success = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (!success) return Result<AttachmentDto>.Failure("Failed to create attachment.");

                var resultDto = new AttachmentDto
                {
                    Id = attachment.Id,
                    Id_Article = attachment.Id_Article,
                    File_name= attachment.File_name,
                    File_type = attachment.File_type,
                    File_size = attachment.File_size,
                    File_path = attachment.File_path

                };

                return Result<AttachmentDto>.Success(resultDto);
            }
        }
    }
}
