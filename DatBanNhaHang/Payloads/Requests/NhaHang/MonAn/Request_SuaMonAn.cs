namespace DatBanNhaHang.Payloads.Requests.NhaHang.MonAn
{
    public class Request_SuaMonAn
    {
        public int LoaiMonAnID { get; set; }
        public string? TenMon { get; set; }
        public string? MoTa { get; set; }
        public double? GiaTien { get; set; }
        public string? AnhMonAn1URL { get; set; }
        public string? AnhMonAn2URL { get; set; }
        public string? AnhMonAn3URL { get; set; }
    }
}
