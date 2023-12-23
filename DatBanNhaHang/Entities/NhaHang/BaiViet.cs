using DatBanNhaHang.Entities.NguoiDung;

namespace DatBanNhaHang.Entities.NhaHang
{
    public class BaiViet : BaseEntity
    {
        public int? AdminId { get; set; }
        public Admin? Admin { get; set; }
        public string? TieuDe { get; set; }
        public string? MoTa { get; set; }
        public DateTime? NgayDang { get; set; }
        public string? AnhBlogURl { get; set; }
        public string? NoiDung { get; set; }

    }
}
