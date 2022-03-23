using System.ComponentModel.DataAnnotations;

namespace SPatrickBack.Authentication
{
    public class LoginModel
    {

        //[Required(ErrorMessage = "User Name is required")]
        //public string Username { get; set; }

        [Required(ErrorMessage = "Dni es requerido")]
        public string Dni { get; set; }

        [Required(ErrorMessage = "Contraseña es requerida")]
        public string Password { get; set; }
    }
}
