using Horyzonty_Nauki.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Horyzonty_Nauki.API.Controllers
{
    public class AttachmentsController : BaseApiController
    {
        private readonly DataContext _context;

        public AttachmentsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var attachments = await _context.Attachments.ToListAsync();
            return Ok(attachments);
        }
    }
}
