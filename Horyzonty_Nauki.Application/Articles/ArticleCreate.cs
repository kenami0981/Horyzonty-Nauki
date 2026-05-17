using FluentValidation;
using Horyzonty_Nauki.Domain;
using Horyzonty_Nauki.Infrastructure.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horyzonty_Nauki.Application.Articles
{
    public class ArticleCreate
    {

        public class Command : IRequest<Result<ArticleDto>>
        {
            public required ArticleCreateDto ArticlesCreateDto { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.ArticlesCreateDto).SetValidator(new ArticleValidator());
            }
        }

        public class Handler : IRequestHandler<Command, Result<ArticleDto>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Result<ArticleDto>> Handle(Command request, CancellationToken cancellationToken)
            {


                var article = new Horyzonty_Nauki.Domain.Article
                {
                    Id = Guid.NewGuid(),
                    Title = request.ArticlesCreateDto.Title,
                    Author = request.ArticlesCreateDto.Author,
                    Pages = request.ArticlesCreateDto.Pages,
                    PublicationDate = request.ArticlesCreateDto.PublicationDate,
                    Category = request.ArticlesCreateDto.Category,
                    OpenCount = request.ArticlesCreateDto.OpenCount,
                    CreatedAt = request.ArticlesCreateDto.CreatedAt,


                };

                _context.Articles.Add(article);
                var success = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (!success) return Result<ArticleDto>.Failure("Failed to create article.");

                var resultDto = new ArticleDto
                {
                    Id = article.Id,
                    Title = article.Title,
                    Author = article.Author,
                    Pages = article.Pages,
                    PublicationDate = article.PublicationDate,
                    Category = article.Category,
                    OpenCount = article.OpenCount,
                    CreatedAt = article.CreatedAt

                };

                return Result<ArticleDto>.Success(resultDto);
            }
        }
    }
}
