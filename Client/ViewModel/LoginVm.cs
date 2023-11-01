using System.ComponentModel.DataAnnotations;

namespace Client.ViewModel
{
    public class LoginVm
    {
        [Required(ErrorMessage ="Email not null!")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password not null!")]
        public string Password { get; set; }
    }
}
