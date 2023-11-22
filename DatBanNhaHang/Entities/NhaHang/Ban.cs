namespace DatBanNhaHang.Entities.NhaHang
{
    public class Ban : BaseEntity
    {
        public string? ViTri { get; set; }
        public int? SoBan {  get; set; }
        public int? SoNguoiToiDa { get; set; }
        public double? GiaTien { get; set; }
        public int? LoaiBanID { get; set; }
        public LoaiBan? LoaiBan { get; set; }
        public int? TrangThaiBanID { get; set; }
        public TrangThaiBan? TrangThaiBan { get; set; }
    }
}
