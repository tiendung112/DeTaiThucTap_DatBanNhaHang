using DatBanNhaHang.Entities.NhaHang;

namespace DatBanNhaHang.Payloads.DTOs.NhaHang
{
    public class ChiTietHoaDonDTOs
    {
        public int ChiTietHoaDonID { get; set; }
        //public int HoaDonID { get; set; }
        //public int? MonAnID { get; set; }
        public int? MonAnID {  get; set; }
        public string? TenMon { get; set; }
        public int? SoLuong { get; set; }
        public string? DonViTinh { get; set; }
        public double? ThanhTien { get; set; }
    }
}
