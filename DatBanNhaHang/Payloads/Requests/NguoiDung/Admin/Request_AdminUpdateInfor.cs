using DatBanNhaHang.Entities.NguoiDung;

namespace DatBanNhaHang.Payloads.Requests.NguoiDung.Admin
{
    public class Request_AdminUpdateInfor
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? gioiTinh { get; set; }
        public DateTime? ngaysinh { get; set; }
        public string? QueQuan { get; set; }
        public string? SDT { get; set; }
    }
}
