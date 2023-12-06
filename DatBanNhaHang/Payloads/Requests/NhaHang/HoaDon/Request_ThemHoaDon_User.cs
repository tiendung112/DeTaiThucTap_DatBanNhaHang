using DatBanNhaHang.Payloads.DTOs.NhaHang;
using DatBanNhaHang.Payloads.Requests.NhaHang.ChiTietHoaDon;

namespace DatBanNhaHang.Payloads.Requests.NhaHang.HoaDon
{
    public class Request_ThemHoaDon_User
    {
        public int? BanID { get; set; }
        public string? TenHoaDon { get; set; }
        public string? GhiChu { get; set; }
        public IList<Request_ThemChiTietHoaDon> ChiTietHoaDonDTOs { get; set; }
    }
}
