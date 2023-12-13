namespace DatBanNhaHang.Payloads.Requests.NguoiDung.Admin.NhanXet
{
    public class Request_SuaNhanXet
    {
        public string? HoTen { get; set; }
        public IFormFile? AnhURL { get; set; }
        public string? NoiDung { get; set; }
        public string? ChuThich { get; set; }
    }
}
