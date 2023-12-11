namespace DatBanNhaHang.Payloads.Requests.NhaHang.Ban
{
    public class Request_SuaBan
    {
        public string? ViTri { get; set; }
        public int? SoBan { get; set; }
        public int? SoNguoiToiDa { get; set; }
        public double? GiaTien { get; set; }
        public int? LoaiBanID { get; set; }
        public string? Mota { get; set; }
        public IFormFile? HinhAnhBanURL { get; set; }
        public string? TinhTrangHienTai { get; set; }
    }
}
