using Microsoft.EntityFrameworkCore;
using SignLinkAPI.Context;
using SignLinkAPI.Models.Auth;
using SignLinkAPI.Models.Tables.UserLayout;
using SignLinkAPI.Utils;

namespace SignLinkAPI.Services
{
    public class Auth
    {
        private readonly UserLayoutDb _Context;
        public Auth(UserLayoutDb Context)
        {
            _Context = Context;
        }

        public async Task<UserAccountDto> GetUserByAuthentication(string Email, string Password)
        {
            try
            {
                var Result = await _Context.UserAccount.FirstOrDefaultAsync(u => u.UserEmail == Email);

                if(Result == null)
                {
                    throw new KeyNotFoundException("User not finded");
                }

                if (!BCrypt.Net.BCrypt.Verify(Password, Result.UserPassword))
                {
                    throw new UnauthorizedAccessException("Incorrect credentials");
                }

                return Result;
            }
            catch(KeyNotFoundException ex)
            {
                throw new KeyNotFoundException("User Not Found:" + ex.Message);
            }
            catch(UnauthorizedAccessException ex)
            {
                throw new UnauthorizedAccessException("Authentication failed: " + ex.Message);
            }
            catch(Exception ex)
            {
                throw new Exception("Unknowed Error: " + ex.Message);
            }
        }

        public async Task<UserAccountDto> GetUserByEmail(string Email)
        {
            var Result = await _Context.UserAccount.FirstOrDefaultAsync(e => e.UserEmail == Email);
            if (Result == null)
            {
                return null;
            }
            return Result;
        }

        public async Task<UserAccountDto> GetUserByUserName(string Username)
        {
            var Result = await _Context.UserAccount.FirstOrDefaultAsync(e => e.UserName == Username);
            if (Result == null)
            {
                return null;
            }
            return Result;
        }

        public async Task<UserAccountDto> CreateUser(CreateAccountInput User)
        {
            try
            {
                if (await GetUserByEmail(User.UserEmail) != null)
                {
                    throw new InvalidOperationException("An account with the email entered alredy exists");
                }
                if (await GetUserByUserName(User.UserName) != null)
                {
                    throw new InvalidOperationException("That Username alredy exists");
                }
                UserAccountDto NewUser = await AuthUtils.CreateNewUserAccount(User);
                await _Context.UserAccount.AddAsync(NewUser);
                await _Context.SaveChangesAsync();

                return NewUser;
            }
            catch(InvalidOperationException ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
            catch(Exception ex)
            {
                throw new Exception("Unknowed error: " + ex.Message);
            }
        }

    }
}
