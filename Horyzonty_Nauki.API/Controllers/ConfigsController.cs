using Horyzonty_Nauki.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Horyzonty_Nauki.API.Controllers
{
    public class ConfigsController:BaseApiController
    {
        private readonly DataContext _context;

        public ConfigsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var configs = await _context.Configs.ToListAsync();
            return Ok(configs);
        }
    }
}
