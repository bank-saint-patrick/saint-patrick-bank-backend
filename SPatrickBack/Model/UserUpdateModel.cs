using System.ComponentModel.DataAnnotations;

namespace SPatrickBack.Authentication
{
    public class UserUpdateModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }


        [Required(ErrorMessage = "Dni is required")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "User Name is required")]
        public string Username { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

    }
}
