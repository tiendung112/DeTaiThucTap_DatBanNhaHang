using System.ComponentModel.DataAnnotations.Schema;

namespace DatBanNhaHang.Entities.NguoiDung
{
    [Table("XacNhanEmail")]
    public class XacNhanEmail : BaseEntity
    {
        public int? UserID { get; set; }
        public User? user { get; set; }
        public int? AdminID { get; set; }
        public Admin? Admin { get; set; }
        public DateTime? ThoiGianHetHan { get; set; }
        public string? MaXacNhan { get; set; }
        public bool DaXacNhan { get; set; } = false;
    }
}
