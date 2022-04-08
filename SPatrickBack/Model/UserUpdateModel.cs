using System.ComponentModel.DataAnnotations;

namespace SPatrickBack.Authentication
{
    public class UserUpdateModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Image { get; set; }

        //[Required(ErrorMessage = "Nombre de usuario es requerido")]
        //public string Username { get; set; }

        [EmailAddress]
       public string Email { get; set; }
       

    }
}
