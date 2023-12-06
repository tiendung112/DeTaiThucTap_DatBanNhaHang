using DatBanNhaHang.Entities.NhaHang;

namespace DatBanNhaHang.Payloads.Requests.NhaHang.ChiTietHoaDon
{
    public class Request_ThemChiTietHoaDon
    {
        public int? MonAnID { get; set; }
        public int? SoLuong { get; set; }
        public string? DonViTinh { get; set; }
    }
}
