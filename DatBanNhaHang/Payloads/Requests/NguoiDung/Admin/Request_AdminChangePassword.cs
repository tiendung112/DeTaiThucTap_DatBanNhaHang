namespace DatBanNhaHang.Payloads.Requests.NguoiDung.Admin
{
    public class Request_AdminChangePassword
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
