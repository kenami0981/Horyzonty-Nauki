using Horyzonty_Nauki.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horyzonty_Nauki.Application.Articles
{
    public class ArticlesList
    {
        public class Query : IRequest<Result<List<ArticleDto>>> { }

        public class Handler : IRequestHandler<Query, Result<List<ArticleDto>>>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<Result<List<ArticleDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var result = await _context.Articles
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
                    .ToListAsync(cancellationToken);

                return Result<List<ArticleDto>>.Success(result);
            }
        }
    }
}
