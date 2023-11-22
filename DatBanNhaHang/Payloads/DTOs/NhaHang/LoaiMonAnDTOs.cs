namespace DatBanNhaHang.Payloads.DTOs.NhaHang
{
    public class LoaiMonAnDTOs
    {
        public int ID { get; set; }
        public string? TenLoai { get; set; }
        public IEnumerable<MonAnDTOs>? MonAn { get; set; }
    }
}
