using Horyzonty_Nauki.Domain;

namespace Horyzonty_Nauki.Application.Articles
{
    public class ArticleDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int Pages { get; set; }
        public DateTime PublicationDate { get; set; }
        public Category Category { get; set; }

        public int OpenCount { get; set; } = 0;
    }
}
