using Horyzonty_Nauki.Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horyzonty_Nauki.Application.Articles
{
    public class ArticleCreateDto
    {
        public string Title { get; set; } = string.Empty;

        public string Author { get; set; } = string.Empty;

        public int Pages { get; set; }

        public DateTime PublicationDate { get; set; }

        public Category Category { get; set; }

        public int OpenCount { get; set; } = 0;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string PdfUrl { get; set; }
        public List<string> Keywords { get; set; } = [];

        public IFormFile ArticleFile { get; set; } = null!;

        public IFormFile? OptionalFile { get; set; }
    }
}
