using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horyzonty_Nauki.Application.Attachments
{
    public class StoredFile
    {
        public string FileName { get; init; } = string.Empty;

        public string FilePath { get; init; } = string.Empty;

        public string ContentType { get; init; } = string.Empty;

        public long FileSize { get; init; }
    }
}
