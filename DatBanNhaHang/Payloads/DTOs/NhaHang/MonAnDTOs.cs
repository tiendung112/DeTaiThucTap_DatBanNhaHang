using DatBanNhaHang.Entities.NhaHang;

namespace DatBanNhaHang.Payloads.DTOs.NhaHang
{
    public class MonAnDTOs
    {
        public int MonAnID { get; set; }
        public int LoaiMonAnID { get; set; }
        public string? TenMon { get; set; }
        public string? MoTa { get; set; }
        public double? GiaTien { get; set; }
        public string? AnhMonAn1URL { get; set; }
        public string? AnhMonAn2URL { get; set; }
        public string? AnhMonAn3URL { get; set; }
    }
}
