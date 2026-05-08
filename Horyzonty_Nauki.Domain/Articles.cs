namespace Horyzonty_Nauki.Domain
{
    public enum Category {M,C,D,P}
    public class Article
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int Pages { get; set; }
        public DateTime PublicationDate { get; set; }
        public Category Category { get; set; }

        public int OpenCount { get; set; } = 0;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
