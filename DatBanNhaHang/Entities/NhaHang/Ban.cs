namespace DatBanNhaHang.Entities.NhaHang
{
    public class Ban : BaseEntity
    {
        public int? SoBan { get; set; }
        public string? ViTri { get; set; }
        public int? SoNguoiToiDa { get; set; }
        public double? GiaTien { get; set; }
        public int? LoaiBanID { get; set; }
        public LoaiBan? LoaiBan { get; set; }
        public string? TrangThaiBan { get; set; }

        // Thông tin mở rộng
        public string? Mota { get; set; }
        public string? HinhAnhBanURL { get; set; }
        public string? TinhTrangHienTai { get; set; }

    }
}
