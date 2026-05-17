using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Horyzonty_Nauki.Application.Articles
{
    public class ArticlesValidator: AbstractValidator<ArticlesCreateDto>
    {
        public ArticlesValidator()
        {
            RuleFor(article => article.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(200).WithMessage("Title is too long (needs to be < 200 characters");
            RuleFor(article => article.Author)
                .NotNull().WithMessage("Author is required.");
            RuleFor(article => article.Pages)
                .NotNull().WithMessage("Page count is required.")
                .GreaterThan(0).WithMessage("Page count must be greater than zero.");
            RuleFor(article => article.PublicationDate).NotNull().WithMessage("Publication date is required.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Publication date cannot be in the future.");
            RuleFor(article=>article.Category)
                .IsInEnum().WithMessage("Category must be a valid value.");
            RuleFor(article => article.OpenCount)
                .GreaterThanOrEqualTo(0).WithMessage("Open count cannot be negative.");
            RuleFor(article => article.CreatedAt)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Created date cannot be in the future.");
        }
    }
}
