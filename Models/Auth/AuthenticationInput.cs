namespace SignLinkAPI.Models.Auth
{
    public class AuthenticationInput
    {
        public string UserEmail { get; set; } = string.Empty;
        public string UserPassword { get; set; } = string.Empty;
    }
}
