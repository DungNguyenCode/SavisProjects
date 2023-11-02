using System.ComponentModel.DataAnnotations;

namespace API.ModelView
{
    public class LoginVm
    {
        [Required]
        [MaxLength(50)]
        public string Email { get; set; }
        [Required]
        [MaxLength(250)]
        public string Password { get; set; }
    }
}
