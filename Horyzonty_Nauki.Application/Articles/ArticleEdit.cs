using FluentValidation;
using Horyzonty_Nauki.Infrastructure.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horyzonty_Nauki.Application.Articles
{
    public class ArticleEdit
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Guid Id { get; set; }
            public required ArticleCreateDto ArticlesCreateDto { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.ArticlesCreateDto).SetValidator(new ArticleValidator());
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
                var article = await _context.Articles.FindAsync(new object[] { request.Id }, cancellationToken);

                if (article == null)
                {
                    return Result<Unit>.Failure("Article not found");
                }

                article.Title = request.ArticlesCreateDto.Title;
                article.Author = request.ArticlesCreateDto.Author;
                article.Pages = request.ArticlesCreateDto.Pages;
                article.PublicationDate = request.ArticlesCreateDto.PublicationDate;
                article.Category = request.ArticlesCreateDto.Category;
                article.OpenCount = request.ArticlesCreateDto.OpenCount;
                article.CreatedAt = request.ArticlesCreateDto.CreatedAt;


                var result = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (!result)
                {
                    return Result<Unit>.Failure("Failed to update article (or no changes detected)");
                }

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
