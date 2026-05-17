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
    public class ConfigsCreate
    {
        public class Command : IRequest<Result<ConfigDto>>
        {
            public required ConfigsCreateDto ConfigsCreateDto { get; set; }
        }
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.ConfigsCreateDto).SetValidator(new ConfigsValidator());
            }
        }
        public class Handler : IRequestHandler<Command, Result<ConfigDto>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Result<ConfigDto>> Handle(Command request, CancellationToken cancellationToken)
            {


                var config= new Horyzonty_Nauki.Domain.Config
                {
                    Id = Guid.NewGuid(),
                    Issn_number = request.ConfigsCreateDto.Issn_number,
                    Logo_path  = request.ConfigsCreateDto.Logo_path

                };

                _context.Configs.Add(config);
                var success = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (!success) return Result<ConfigDto>.Failure("Failed to create config.");

                var resultDto = new ConfigDto
                {
                    Id = config.Id,
                    Issn_number = config.Issn_number,
                    Logo_path = config.Logo_path,

                };

                return Result<ConfigDto>.Success(resultDto);
            }
        }

    }
}
