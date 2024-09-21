using Microsoft.EntityFrameworkCore;
using SignLinkAPI.Models.Tables.UserLayout;

namespace SignLinkAPI.Context
{
    public class UserLayoutDb : DbContext
    {
        public UserLayoutDb(DbContextOptions options) : base(options) { }

        public DbSet<UserAccountDto> UserAccount { get; set; }
    }
}
