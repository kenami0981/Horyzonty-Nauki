using System.ComponentModel.DataAnnotations;

namespace Horyzonty_Nauki.Domain
{
    public class Administrator
    {
        public Guid Id { get; set; }
        public string Login { get; set; } 
        public string Password { get; set; }
        public string Email { get; set; }

    }
}
