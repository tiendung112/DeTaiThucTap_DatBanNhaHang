    namespace DatBanNhaHang.Entities.NhaHang
{
    public class HoaDon : BaseEntity
    {
        public int KhachHangID { get; set; }
        public KhachHang? KhachHang { get; set; }
        public int? BanID { get; set; }
        public Ban? Ban { get; set; }
        public int? TrangThaiHoaDonID { get; set; }
        public TrangThaiHoaDon? TrangThaiHoaDon { get; set; }
        public string? TenHoaDon { get; set; }
        public string? MaGiaoDich { get; set; }
        public DateTime? ThoiGianTao { get; set; }
        public DateTime? ThoiGianCapNhap { get; set; }
        public string? GhiChu { get; set; }
        public double? TongTien { get; set; }

        public IList<ChiTietHoaDon>? chiTietHoaDon { get; set; }
    }
}
