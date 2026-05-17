using FluentValidation;
using Horyzonty_Nauki.Application.Attachments;
using Horyzonty_Nauki.Infrastructure.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horyzonty_Nauki.Application.Configs
{
    public class ConfigEdit
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Guid Id { get; set; }
            public required ConfigCreateDto ConfigsCreateDto { get; set; }
        }
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.ConfigsCreateDto).SetValidator(new ConfigValidator());
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
                var config = await _context.Configs.FindAsync(new object[] { request.Id }, cancellationToken);

                if (config == null)
                {
                    return Result<Unit>.Failure("Config not found");
                }
                config.Id = request.ConfigsCreateDto.Id;
                config.Issn_number = request.ConfigsCreateDto.Issn_number;
                config.Logo_path = request.ConfigsCreateDto.Logo_path;



                var result = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (!result)
                {
                    return Result<Unit>.Failure("Failed to update confgig (or no changes detected)");
                }


                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
