using FluentValidation;
using Horyzonty_Nauki.Application.Articles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horyzonty_Nauki.Application.Attachments
{
    public class AttachmentValidator : AbstractValidator<AttachmentDto>
    {
        public AttachmentValidator() { 
            RuleFor(attachment => attachment.Id)
                .NotEmpty().WithMessage("Attachment ID is required.");
            RuleFor(attachment => attachment.Id_Article)
                .NotEmpty().WithMessage("Article ID is required.");
            RuleFor(attachment => attachment.File_name)
                .NotEmpty().WithMessage("File name is required.")
                .MaximumLength(255).WithMessage("File name is too long (needs to be < 255 characters).");
            RuleFor(attachment => attachment.File_type)
                .NotEmpty().WithMessage("File type is required.")
                .MaximumLength(100).WithMessage("File type is too long (needs to be < 100 characters).");
            RuleFor(attachment => attachment.File_size)
                .GreaterThan(0).WithMessage("File size must be greater than zero.");
            RuleFor(attachment => attachment.File_path)
                .NotEmpty().WithMessage("File path is required.")
                .MaximumLength(500).WithMessage("File path is too long (needs to be < 500 characters).");
        }
    }
}
