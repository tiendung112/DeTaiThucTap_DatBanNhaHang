namespace DatBanNhaHang.Entities.NhaHang
{
    public class TrangThaiHoaDon : BaseEntity
    {
        public string? TenTrangThai { get; set; }
        public IList<HoaDon>? hoaDon { get; set; }
    }
}
