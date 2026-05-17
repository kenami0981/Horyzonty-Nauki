using Horyzonty_Nauki.Infrastructure.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horyzonty_Nauki.Application.Articles
{
    public class ArticleDelete
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
                var article = await _context.Articles.FindAsync(request.Id);
                if (article== null)
                {
                    return Result<Unit>.Failure("Article not found");
                }
                _context.Articles.Remove(article);
                var success = await _context.SaveChangesAsync(cancellationToken) > 0;
                if (!success)
                {
                    return Result<Unit>.Failure("Failed to delete the article");
                }
                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
