namespace SignLinkAPI.Models.Auth
{
    public class CreateAccountInput
    {
        public string UserEmail { get; set; } = string.Empty;
        public string UserPassword { get; set; } = string.Empty;
        public string UserProfilePicUrl { get; set; } = string.Empty;
        public string UserBio { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
    }
}
