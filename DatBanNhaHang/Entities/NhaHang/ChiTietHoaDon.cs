namespace DatBanNhaHang.Entities.NhaHang
{
    public class ChiTietHoaDon : BaseEntity
    {
        public int HoaDonID { get; set; }
        public HoaDon? HoaDon { get; set; }
        public int? MonAnID { get; set; }
        public MonAn? monAn { get; set; }
        public int? SoLuong { get; set; }
        public double? ThanhTien { get; set; }
    }
}
