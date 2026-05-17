using Horyzonty_Nauki.Infrastructure.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horyzonty_Nauki.Application.Configs
{
    public class ConfigsDelete
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
                var config = await _context.Configs.FindAsync(request.Id);
                if (config == null)
                {
                    return Result<Unit>.Failure("Config not found");
                }
                _context.Configs.Remove(config);
                var success = await _context.SaveChangesAsync(cancellationToken) > 0;
                if (!success)
                {
                    return Result<Unit>.Failure("Failed to delete the config");
                }
                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
