namespace DatBanNhaHang.Payloads.DTOs.NguoiDung
{
    public class UserDTO
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string AvatarUrl { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
