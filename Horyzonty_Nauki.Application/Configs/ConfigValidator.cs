using FluentValidation;
using Horyzonty_Nauki.Application.Attachments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horyzonty_Nauki.Application.Configs
{
    public class ConfigValidator : AbstractValidator<ConfigCreateDto>
    {
        public ConfigValidator() { 
            RuleFor(config => config.Id)
                .NotEmpty().WithMessage("Config ID is required.");
            RuleFor(config => config.Issn_number)
                .NotEmpty().WithMessage("Issn number is requiered.")
                .InclusiveBetween(10000000, 99999999).WithMessage("Issn number must be an 8-digit number.");

            RuleFor(config => config.Logo_path)
                .NotEmpty().WithMessage("Logo path is required.");

        }
    }
}
