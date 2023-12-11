using DatBanNhaHang.Entities.NguoiDung;

namespace DatBanNhaHang.Payloads.DTOs.NguoiDung
{
    public class AdminDTOs
    {
        public int? ADminID {  get; set; }
        public string? AdminName { get; set; }
        //public string? password { get; set; }
        public string? Name { get; set; }
        public string? Gender { get; set; }
        public DateTime? ngaysinh { get; set; }
        public string? QueQuan { get; set; }
        public string? Email { get; set; }
        public string? SDT { get; set; }
        public DateTime? create_at { get; set; }
        public string? RoleName { get; set; }
        public string? NguoiTao {  get; set; }
    }
}
