using DatBanNhaHang.Payloads.Requests.NhaHang.ChiTietHoaDon;

namespace DatBanNhaHang.Payloads.Requests.NhaHang.HoaDon
{
    public class Request_ThemHoaDon_Admin
    {
        public int? Khid {  get; set; }
        public int? BanID { get; set; }
        // public string? TenHoaDon { get; set; }
        public string? GhiChu { get; set; }
        // Thời gian liên quan đến đặt bàn
        public DateTime ThoiGianDuKienBatDau { get; set; } // Thời gian dự kiến bắt đầu
        public IEnumerable<Request_ThemChiTietHoaDon>? ChiTietHoaDonDTOs { get; set; }
    }
}
