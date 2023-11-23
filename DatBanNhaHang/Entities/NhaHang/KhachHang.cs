using System.ComponentModel.DataAnnotations.Schema;

namespace DatBanNhaHang.Entities.NhaHang
{
    public class KhachHang : BaseEntity
    {
        public string? HoTen { get; set; }
        // [Column(TypeName = "date")]
        public DateTime? NgaySinh { get; set; }
        public string? DiaChi { get; set; }
        public string? SDT { get; set; }
        public IList<HoaDon>? hoaDon { get; set; }

    }
}
