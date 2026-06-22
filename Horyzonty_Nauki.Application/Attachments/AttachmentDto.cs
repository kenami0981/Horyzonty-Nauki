using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horyzonty_Nauki.Application.Attachments
{
    public class AttachmentDto
    {
        public Guid Id { get; set; }

        public Guid Id_Article { get; set; }

        public string File_name { get; set; }
        public string File_type { get; set; }

        public long File_size { get; set; }

        public string File_path { get; set; }
    }
}
