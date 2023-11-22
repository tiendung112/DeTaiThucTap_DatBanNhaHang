namespace DatBanNhaHang.Entities.NhaHang
{
    public class TrangThaiBan : BaseEntity
    {
        public string? TenTrangThai { get; set; }
        public IList<Ban>? ban { get; set; }
    }
}
