using Horyzonty_Nauki.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Horyzonty_Nauki.Application.Articles
{
    public class ArticleIncreaseOpenCountHandler
        : IRequestHandler<ArticleIncreaseOpenCount.Command, Result<Unit>>
    {
        private readonly DataContext _context;

        public ArticleIncreaseOpenCountHandler(DataContext context)
        {
            _context = context;
        }

        public async Task<Result<Unit>> Handle(
            ArticleIncreaseOpenCount.Command request,
            CancellationToken cancellationToken)
        {
            var article = await _context.Articles
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (article == null)
                return Result<Unit>.Failure("Not found");

            article.OpenCount++;

            await _context.SaveChangesAsync(cancellationToken);

            return Result<Unit>.Success(Unit.Value);
        }
    }
}