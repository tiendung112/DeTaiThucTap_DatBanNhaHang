using DatBanNhaHang.Services.Implements.DatBanNhaHang.Service.Implements;

namespace DatBanNhaHang.Entities.NhaHang
{
    public class LienHe : BaseEntity
    {
        public string? Hoten { get; set; }
        public string? Email { get; set; }
        public string? TieuDe { get; set; }
        public DateTime? ThoiGianGui {  get; set; }
        public bool? DaTraLoi { get; set; }
        public DateTime? ThoiGianTraLoi { get; set; }
        public string? NoiDung { get; set; }
    }
}
