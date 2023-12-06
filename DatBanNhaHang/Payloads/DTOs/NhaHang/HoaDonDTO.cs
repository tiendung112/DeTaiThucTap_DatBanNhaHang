using DatBanNhaHang.Entities.NhaHang;

namespace DatBanNhaHang.Payloads.DTOs.NhaHang
{
    public class HoaDonDTO
    {
        public int HoaDonID { get; set; }
        public int KhachHangID { get; set; }
        public string TenKhachHang { get; set; }
        public int? BanID { get; set; }
        public int? TrangThaiHoaDonID { get; set; }
        public string? TenHoaDon { get; set; }
        public string? MaGiaoDich { get; set; }
        public DateTime? ThoiGianTao { get; set; }
        public DateTime? ThoiGianCapNhap { get; set; }
        public string? GhiChu { get; set; }
        public double? TongTien { get; set; }
        public IQueryable<ChiTietHoaDonDTOs> ChiTietHoaDonDTOs { get; set; }
    }
}
