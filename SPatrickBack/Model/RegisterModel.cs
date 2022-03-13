using System.ComponentModel.DataAnnotations;

namespace SPatrickBack.Authentication
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Dni is required")]
        public string Dni { get; set; }

        [Required(ErrorMessage = "User Name is required")]
        public string Username { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
