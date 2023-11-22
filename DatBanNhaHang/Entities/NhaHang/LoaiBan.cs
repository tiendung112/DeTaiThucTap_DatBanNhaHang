namespace DatBanNhaHang.Entities.NhaHang
{
    public class LoaiBan : BaseEntity
    {
        public string? TenLoaiBan { get; set; }
        public IList<Ban>? ban { get; set; }
    }
}
