namespace DatBanNhaHang.Payloads.Requests.NguoiDung
{
    public class Request_Register
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public IFormFile AvatarUrl { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
