using DatBanNhaHang.Entities.NhaHang;

namespace DatBanNhaHang.Payloads.Requests.NhaHang.KhachHang
{
    public class Request_ThemKhachHang
    {
        public string? HoTen { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string? DiaChi { get; set; }
        public string? SDT { get; set; }
        public int? userID {  get; set; }

    }
}
