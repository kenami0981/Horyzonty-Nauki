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

            context.Articles.Add(article);
            await context.SaveChangesAsync();
        }
    }
}
