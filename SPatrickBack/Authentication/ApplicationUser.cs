using Microsoft.AspNetCore.Identity;

namespace SPatrickBack.Authentication
{
    public class ApplicationUser:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
