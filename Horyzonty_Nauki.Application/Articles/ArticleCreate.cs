using FluentValidation;
using Horyzonty_Nauki.Application.Attachments;
using Horyzonty_Nauki.Domain;
using Horyzonty_Nauki.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Horyzonty_Nauki.Application.Articles
{
    public class ArticleCreate
    {

        public class Command : IRequest<Result<ArticleDto>>
        {
            public required ArticleCreateDto ArticleCreateDto { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.ArticleCreateDto).SetValidator(new ArticleValidator());
            }
        }

        public class Handler : IRequestHandler<Command, Result<ArticleDto>>
        {
            private readonly DataContext _context;
            private readonly IFileStorageService _storageService;

            public Handler(DataContext context, IFileStorageService storageService)
            {
                _context = context;
                _storageService = storageService;
            }

            public async Task<Result<ArticleDto>> Handle(
                Command request,
                CancellationToken cancellationToken)
            {
                var dto = request.ArticleCreateDto;

                var article = new Domain.Article
                {
                    Id = Guid.NewGuid(),
                    Title = dto.Title,
                    Author = dto.Author,
                    Pages = dto.Pages,
                    PublicationDate = dto.PublicationDate,
                    Category = dto.Category,
                    OpenCount = 0,
                    CreatedAt = DateTime.UtcNow
                };

                _context.Articles.Add(article);

                var count = await _context.Articles
                    .CountAsync(
                        a => a.PublicationDate.Year == dto.PublicationDate.Year,
                        cancellationToken
                    );
                string text = (count + 1).ToString("D3");
                string filePath = dto.Category.ToString() + "-" + dto.PublicationDate.ToString("yy") + "-" + text;

                var mainFile = await _storageService.SaveAsync(dto.ArticleFile, filePath + ".pdf", cancellationToken);

                _context.Attachments.Add(new Attachment
                {
                    Id = Guid.NewGuid(),
                    Id_Article = article.Id,
                    File_name = mainFile.FileName,
                    File_type = mainFile.ContentType,
                    File_size = mainFile.FileSize,
                    File_path = mainFile.FilePath
                });

                if (dto.OptionalFile != null)
                {
                    var optFile = await _storageService.SaveAsync(dto.OptionalFile, filePath + "_optional" + dto.OptionalFile.ContentType, cancellationToken);

                    _context.Attachments.Add(new Attachment
                    {
                        Id = Guid.NewGuid(),
                        Id_Article = article.Id,
                        File_name = mainFile.FileName,
                        File_type = mainFile.ContentType,
                        File_size = mainFile.FileSize,
                        File_path = mainFile.FilePath
                    });
                }

                var success = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (!success)
                    return Result<ArticleDto>.Failure("Failed to save an article");

                var result = new ArticleDto
                {
                    Id = article.Id,
                    Title = article.Title,
                    Author = article.Author,
                    Pages = article.Pages,
                    PublicationDate = article.PublicationDate,
                    Category = article.Category,
                    OpenCount = article.OpenCount
                };

                return Result<ArticleDto>.Success(result);
            }
        }
    }
}
