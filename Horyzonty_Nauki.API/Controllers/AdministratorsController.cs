using Horyzonty_Nauki.Domain;
using Horyzonty_Nauki.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Horyzonty_Nauki.API.Controllers
{
    public class AdministratorsController:BaseApiController
    {
        private readonly DataContext _context;
        private readonly TokenGenerator _tokenGenerator;

        public AdministratorsController(DataContext context, TokenGenerator tokenGenerator)
        {
            _context = context;
            _tokenGenerator = tokenGenerator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _context.Administrators.FirstOrDefaultAsync(x => x.Email == dto.Email);

            if (user == null)
                return Unauthorized();

            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
                return Unauthorized();

            var token = _tokenGenerator.GenerateJwtToken(user);

            return Ok(new { token });
        }

    }
}
