using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SignLinkAPI.Models.Tables.UserLayout
{
    [Table("user_account")]
    public class UserAccountDto
    {
        [Column("user_id")]
        [Key]
        public int UserId { get; set; }
        [Column("username")]
        [MaxLength(50)]
        public string UserName { get; set; } = string.Empty;
        [Column("user_email")]
        [MaxLength(100)]
        public string UserEmail { get; set; } = string.Empty;
        [Column("user_hash_password")]
        [MaxLength(255)]
        public string UserPassword { get; set; } = string.Empty;
        [Column("user_profile_picture_url")]
        [MaxLength(255)]
        public string UserProfilePicture { get; set; } = string.Empty;
        [Column("bio")]
        public string UserBio { get; set; } = string.Empty;
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
    }
}
