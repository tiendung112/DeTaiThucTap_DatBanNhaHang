namespace DatBanNhaHang.Payloads.Requests.NguoiDung.User
{
    public class Request_Register
    {
        public string? UserName { get; set; }
        public string? Name { get; set; }
        public string? Gender { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? SDT { get; set; }
        public string? address { get; set; }
        //public IFormFile? AvatarUrl { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
