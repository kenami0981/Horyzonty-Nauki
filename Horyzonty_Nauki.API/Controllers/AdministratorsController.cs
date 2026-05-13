using Horyzonty_Nauki.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Horyzonty_Nauki.API.Controllers
{
    public class AdministratorsController:BaseApiController
    {
        private readonly DataContext _context;

        public AdministratorsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var administrators= await _context.Configs.ToListAsync();
            return Ok(administrators);
        }
    }
}
