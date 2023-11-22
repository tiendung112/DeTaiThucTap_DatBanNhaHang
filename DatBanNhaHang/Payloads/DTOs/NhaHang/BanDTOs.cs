using DatBanNhaHang.Entities.NhaHang;

namespace DatBanNhaHang.Payloads.DTOs.NhaHang
{
    public class BanDTOs
    {
        public int BanID   { get; set; }
        public string? ViTri { get; set; }
        public int? SoBan { get; set; }
        public int? SoNguoiToiDa { get; set; }
        public double? GiaTien { get; set; }
        public int? LoaiBanID { get; set; }
        public int? TrangThaiBanID { get; set; }
    }
}
