

namespace DatBanNhaHang.Payloads.Requests.NguoiDung.User
{
    public class Request_UpdateInfor
    {
        public int UserID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Gender { get; set; }
        public IFormFile? AvatarUrl { get; set; }

        public DateTime? DateOfBirth { get; set; }

    }
}
