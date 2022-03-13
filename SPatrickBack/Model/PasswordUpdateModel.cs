namespace SPatrickBack.Authentication
{
    public class PasswordUpdateModel
    {
        public string Email { get; set; }
        public string currentPassword { get; set; } 
        public string newPassword { get; set; }
    }
}
