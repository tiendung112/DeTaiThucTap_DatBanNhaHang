using DatBanNhaHang.Entities.NhaHang;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatBanNhaHang.Entities.NguoiDung
{
    [Table("Admin")]
    [Index("AdminName", IsUnique = true)]
    public class Admin :BaseEntity
    {
        public string? AdminName { get; set; }
        public string? password { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Gender { get; set; }
        public DateTime? ngaysinh { get; set; }
        public string? QueQuan {  get; set; }
        public string? SDT {  get; set; }
        public DateTime? create_at { get; set; }
        public int? RoleID { get; set; }
        public Role? Role { get; set; }
        public IEnumerable<XacNhanEmail>? emails { get; set; }
        public IEnumerable<RefreshToken>? RefreshTokens { get; set; }
        //đời sau 
        public int? ParentID { get; set; }
        public Admin? Parent { get; set; }
        public IEnumerable<Admin>? Children { get; set; }
        public IEnumerable<BaiViet>? bLogs { get; set; }
    }
}
