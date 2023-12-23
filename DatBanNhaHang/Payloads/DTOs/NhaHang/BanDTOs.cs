namespace DatBanNhaHang.Payloads.DTOs.NhaHang
{
    public class BanDTOs
    {
        public int BanID { get; set; }
        public string? ViTri { get; set; }
        public int? SoBan { get; set; }
        public int? SoNguoiToiDa { get; set; }
        public double? GiaTien { get; set; }
        public int? LoaiBanID { get; set; }
        public string? TenLoaiBan { get; set; }
        public string? Mota { get; set; }
        public string? HinhAnhBanURL { get; set; }
        public string? TinhTrangHienTai { get; set; }
    }
}
