namespace DatBanNhaHang.Payloads.Requests.NguoiDung.Admin.Blog
{
    public class Request_ThemBaiViet
    {
        public string? TieuDe { get; set; }
        public string? MoTa { get; set; }
        public DateTime? NgayDang { get; set; }
        public IFormFile? AnhBlogURl { get; set; }
        public string? NoiDung { get; set; }
    }
}
