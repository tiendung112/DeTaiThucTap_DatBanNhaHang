using DatBanNhaHang.Entities.NhaHang;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatBanNhaHang.Entities.NguoiDung
{
    [Table("User")]
    [Index("UserName", IsUnique = true)]
    public class User : BaseEntity
    {
        public string? UserName { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Gender { get; set; }
        public string? AvatarUrl { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? SDT { get; set; }
        public string? address { get; set; }
        public DateTime? ngayTao { get; set; }
        public bool? IsActive { get; set; } = false;
        public IEnumerable<RefreshToken>? RefreshTokens { get; set; }
        public IEnumerable<XacNhanEmail>? xacNhanEmails { get; set; }
        //public IEnumerable<KhachHang> khachHangs { get; set; }

    }
}
