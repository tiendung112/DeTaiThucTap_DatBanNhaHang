using DatBanNhaHang.Entities.NguoiDung;

namespace DatBanNhaHang.Payloads.DTOs.NguoiDung
{
    public class BaiVietDTOs
    {
        public int? BlogID { get; set; }
        public string? TenNguoiDang { get; set; }
        public string? TieuDe { get; set; }
        public string? MoTa { get; set; }
        public DateTime? NgayDang { get; set; }
        public string? AnhBlogURl { get; set; }
        public string? NoiDung { get; set; }
        
    }
}
