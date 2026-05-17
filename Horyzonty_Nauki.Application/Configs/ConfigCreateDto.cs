using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horyzonty_Nauki.Application.Configs
{
    public class ConfigCreateDto
    {
        public Guid Id { get; set; }
        public int Issn_number { get; set; }
        public string Logo_path { get; set; }
    }
}
