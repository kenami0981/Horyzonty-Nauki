using Horyzonty_Nauki.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horyzonty_Nauki.Infrastructure.Data
{
    public static class Seed
    {
        public static async Task SeedData(DataContext context)
        {
            if (context.Articles.Any()) return;

            var article = new Article
            {
                Id = Guid.NewGuid(),
                Title = "Pierwszy artykuł testowy",
                Author = "Jan Kowalski",
                Pages = 10,
                PublicationDate = DateTime.UtcNow,
                Category = Category.M,
                OpenCount = 15,
                CreatedAt = DateTime.UtcNow
            };

            var attachment = new Attachment
            {
                Id = Guid.NewGuid(),
                Id_Article = article.Id,
                File_name= "test.pdf",
                File_type = "application/pdf",
                File_size= 1024,
            };

            context.Articles.Add(article);
            context.Attachments.Add(attachment);
            await context.SaveChangesAsync();
        }
    }
}
