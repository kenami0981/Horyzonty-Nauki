using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horyzonty_Nauki.Application.Attachments
{
    public class StoredFileResponse
    {
        public Stream Content { get; set; } = null!;

        public string FileName { get; set; } = string.Empty;

        public string ContentType { get; set; } = "application/octet-stream";
    }
}
