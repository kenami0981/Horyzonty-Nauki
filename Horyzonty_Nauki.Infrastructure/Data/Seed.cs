using Horyzonty_Nauki.Domain;

namespace Horyzonty_Nauki.Infrastructure.Data
{
    public static class Seed
    {
        public static async Task SeedData(DataContext context)
        {
            bool changed = false;

            if (!context.Articles.Any())
            {
                var article = new Article
                {
                    Id = Guid.NewGuid(),
                    Title = "Pierwszy artykuł testowy",
                    Author = "Jan Kowalski",
                    Pages = 10,
                    PublicationDate = DateTime.UtcNow,
                    Category = Category.M,
                    OpenCount = 15,
                    CreatedAt = DateTime.UtcNow,
                    
                };

                var attachment = new Attachment
                {
                    Id = Guid.NewGuid(),
                    Id_Article = article.Id,
                    File_name = "test.pdf",
                    File_type = "application/pdf",
                    File_size = 1024,
                    File_path = "test.pdf",
                };

                context.Articles.Add(article);
                context.Attachments.Add(attachment);
                changed = true;
            }

            if (!context.Administrators.Any())
            {
                var administrator = new Administrator
                {
                    Id = Guid.NewGuid(),
                    Login = "Testowy",
                    Password = BCrypt.Net.BCrypt.HashPassword("Administrator"),
                    Email = "example@ex.com",
                };

                context.Administrators.Add(administrator);
                changed = true;
            }

            if (!context.Configs.Any())
            {
                var config = new Config
                {
                    Id = Guid.NewGuid(),
                    Issn_number = 23153872,
                    Logo_path = "examplePath"
                };

                context.Configs.Add(config);
                changed = true;
            }

            if (changed)
            {
                await context.SaveChangesAsync();
            }
        }
    }
}
