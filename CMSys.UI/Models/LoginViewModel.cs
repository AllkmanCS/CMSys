namespace CMSys.UI.Models
{
    public class LoginViewModel
    {
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public string PasswordSalt { get; private set; }
    }
}
