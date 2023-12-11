using DatBanNhaHang.Entities.NhaHang;

namespace DatBanNhaHang.Payloads.DTOs.NhaHang
{
    public class TrangThaiHoaDonDTOs
    {
        public int TrangThaiHoaDonID { get; set; }
        public string? TenTrangThai {  get; set; }
        public IEnumerable<HoaDonDTO>? hoaDons { get; set; }
    }
}
