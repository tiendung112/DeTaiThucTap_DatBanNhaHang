using DatBanNhaHang.Entities.NguoiDung;

namespace DatBanNhaHang.Payloads.DTOs.NguoiDung
{
    public class ProfileUserDTOs
    {
        public int UserID { get; set; }
        public string? UserName { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Gender { get; set; }
        public string? AvatarUrl { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? address { get; set; }
        public string? SDT { get; set; }


    }
}
