namespace DatBanNhaHang.Payloads.Requests.NguoiDung
{
    public class Request_ChangePassword
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
