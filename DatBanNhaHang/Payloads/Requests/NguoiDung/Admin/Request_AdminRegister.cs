using DatBanNhaHang.Entities.NguoiDung;

namespace DatBanNhaHang.Payloads.Requests.NguoiDung.Admin
{
    public class Request_AdminRegister
    {
        public string? AdminName { get; set; }
        public string? password { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Gender { get; set; }
        public DateTime? ngaysinh { get; set; }
        public string? QueQuan { get; set; }
        public string? SDT { get; set; }
        public int? RoleID { get; set; }
    }
}
