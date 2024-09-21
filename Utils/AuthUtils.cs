using SignLinkAPI.Models.Auth;
using SignLinkAPI.Models.Tables.UserLayout;

namespace SignLinkAPI.Utils
{
    public class AuthUtils
    {
        public static async Task<UserAccountDto> CreateNewUserAccount(CreateAccountInput Input)
        {
            UserAccountDto NewUser = new UserAccountDto
            {
                UserEmail = Input.UserEmail,
                UserPassword = BCrypt.Net.BCrypt.HashPassword(Input.UserPassword),
                UserName = Input.UserName,
                UserBio = Input.UserBio,
                UserProfilePicture = Input.UserProfilePicUrl,
                CreatedAt = DateTime.UtcNow
            };

            return NewUser;
        }
    }
}
