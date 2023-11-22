namespace DatBanNhaHang.Payloads.Requests.NhaHang.ChiTietHoaDon
{
    public class Request_SuaChiTietHoaDon
    {
        public int ChiTietHoaDonID { get; set; }
        public int HoaDonID { get; set; }
        public int? MonAnID { get; set; }
        public int? SoLuong { get; set; }
        public string? DonViTinh { get; set; }
    }
}
