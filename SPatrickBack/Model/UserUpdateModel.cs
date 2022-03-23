using System.ComponentModel.DataAnnotations;

namespace SPatrickBack.Authentication
{
    public class UserUpdateModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required(ErrorMessage = "Dni es requerido")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Nombre de usuario es requerido")]
        public string Username { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email es requerido")]
        public string Email { get; set; }

    }
}
