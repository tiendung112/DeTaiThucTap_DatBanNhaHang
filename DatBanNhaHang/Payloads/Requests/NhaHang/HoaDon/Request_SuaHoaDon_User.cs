using DatBanNhaHang.Payloads.Requests.NhaHang.ChiTietHoaDon;

namespace DatBanNhaHang.Payloads.Requests.NhaHang.HoaDon
{
    public class Request_SuaHoaDon_User
    {

        public string? GhiChu { get; set; }
        // Thời gian liên quan đến đặt bàn
        public DateTime ThoiGianDuKienBatDau { get; set; } // Thời gian dự kiến bắt đầu
        public IEnumerable<Request_ThemChiTietHoaDon>? ChiTietHoaDonDTOs { get; set; }
    }
}
