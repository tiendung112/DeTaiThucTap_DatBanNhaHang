

namespace DatBanNhaHang.Payloads.Requests.NguoiDung.User
{
    public class Request_UpdateInfor
    {
        //public string UserName { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Gender { get; set; }
        public IFormFile? AvatarUrl { get; set; }
        public DateTime? DateOfBirth { get; set; }

    }
}
