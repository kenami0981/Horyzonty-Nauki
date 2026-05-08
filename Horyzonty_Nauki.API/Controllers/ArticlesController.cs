using Horyzonty_Nauki.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Horyzonty_Nauki.API.Controllers
{
    public class ArticlesController : BaseApiController
    {
        private readonly DataContext _context;

        public ArticlesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var articles = await _context.Articles.ToListAsync();
            return Ok(articles);
        }
    }
}