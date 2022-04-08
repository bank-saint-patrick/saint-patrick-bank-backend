using System.ComponentModel.DataAnnotations;

namespace SPatrickBack.Authentication
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Documento es requerido")]
        public string Dni { get; set; }

        [Required(ErrorMessage = "Nombre es requerido")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Apellido es requerido")]
        public string LastName { get; set; }

        //[Required(ErrorMessage = "User Name is required")]
        //public string Username { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email es requerido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Telefono es requerido")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Contraseña es requerido")]
        public string Password { get; set; }

        public string Image { get; set; }
    }
}
