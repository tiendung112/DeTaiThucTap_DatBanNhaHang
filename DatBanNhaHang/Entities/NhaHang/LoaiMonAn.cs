namespace DatBanNhaHang.Entities.NhaHang
{
    public class LoaiMonAn : BaseEntity
    {
        public string? TenLoai { get; set; }
        public IEnumerable<MonAn>? MonAn { get; set; }
    }
}
