using Horyzonty_Nauki.Application;
using Horyzonty_Nauki.Application.Articles;
using Horyzonty_Nauki.Domain;
using Horyzonty_Nauki.Infrastructure;
using Horyzonty_Nauki.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horyzonty_Nauki.Application.Article
{
    public class ArticleDetails
    {
        public class Query : IRequest<Result<ArticleDto>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<ArticleDto>>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<Result<ArticleDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var articleDto = await _context.Articles
                    .Where(b => b.Id == request.Id)
                    .Select(b => new ArticleDto
                    {
                        Id = b.Id,
                        Title = b.Title,
                        Author = b.Author,
                        Pages = b.Pages,
                        PublicationDate = b.PublicationDate,
                        Category = b.Category,
                        OpenCount = b.OpenCount,
                        CreatedAt = b.CreatedAt
                    })
                    .FirstOrDefaultAsync(cancellationToken);

                if (articleDto == null)
                {
                    return Result<ArticleDto>.Failure("Article not found");
                }

                return Result<ArticleDto>.Success(articleDto);
            }
        }
    }
}
