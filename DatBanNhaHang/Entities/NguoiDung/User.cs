using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace DatBanNhaHang.Entities.NguoiDung
{
    [Table("User")]
    [Index("UserName", IsUnique = true)]
    public class User : BaseEntity
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Gender { get; set; }
        public string? AvatarUrl { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? ngayTao { get; set; }
        public bool? IsActive { get; set; } = false;
        public IEnumerable<RefreshToken>? RefreshTokens { get; set; }
        public IEnumerable<XacNhanEmail>? xacNhanEmails {  get; set; } 

    }
}
