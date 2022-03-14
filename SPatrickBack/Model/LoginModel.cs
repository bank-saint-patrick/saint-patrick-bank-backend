using System.ComponentModel.DataAnnotations;

namespace SPatrickBack.Authentication
{
    public class LoginModel
    {

        //[Required(ErrorMessage = "User Name is required")]
        //public string Username { get; set; }

        [Required(ErrorMessage = "Dni is required")]
        public string Dni { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
