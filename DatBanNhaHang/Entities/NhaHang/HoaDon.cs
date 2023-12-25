using DatBanNhaHang.Entities.NguoiDung;

namespace DatBanNhaHang.Entities.NhaHang
{
    public class HoaDon : BaseEntity
    {
        public int userId { get; set; }
        public User? KhachHang { get; set; }
        public int? BanID { get; set; }
        public Ban? Ban { get; set; }
        public int? TrangThaiHoaDonID { get; set; }
        public TrangThaiHoaDon? TrangThaiHoaDon { get; set; }
        public string? TenHoaDon { get; set; }
        public string? MaGiaoDich { get; set; }

        // Thời gian liên quan đến đặt bàn
        public DateTime? ThoiGianDat { get; set; } // Thời gian đặt bàn
        public DateTime? ThoiGianDuKienBatDau { get; set; } // Thời gian dự kiến bắt đầu
        public DateTime? ThoiGianDuKienKetThuc { get; set; } // Thời gian dự kiến kết thúc
        public DateTime? ThoiGianBatDauThucTe { get; set; } // Thời gian bắt đầu thực tế (nullable)
        public DateTime? ThoiGianKetThucThucTe { get; set; } // Thời gian kết thúc thực tế (nullable)
        public DateTime? ThoiGianHuyDat { get; set; } // Thời gian hủy đặt bàn (nullable)
        public string? GhiChu { get; set; }
        public double? TongTien { get; set; }
    }
}
