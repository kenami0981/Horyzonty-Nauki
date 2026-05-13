

using System.ComponentModel.DataAnnotations.Schema;

namespace Horyzonty_Nauki.Domain
{
    public class Attachment
    {
        public Guid Id { get; set; }

        [ForeignKey("Article")]
        public int Id_Article { get; set; }

        public string File_name { get; set; }
        public string File_type { get; set; }

        public int File_size { get; set; }

        public string File_path { get; set; }
    }
}
