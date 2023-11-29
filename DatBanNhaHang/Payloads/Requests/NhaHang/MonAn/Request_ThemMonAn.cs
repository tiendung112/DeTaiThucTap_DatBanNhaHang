namespace DatBanNhaHang.Payloads.Requests.NhaHang.MonAn
{
    public class Request_ThemMonAn
    {
        public int LoaiMonAnID { get; set; }
        public string? TenMon { get; set; }
        public string? MoTa { get; set; }
        public double? GiaTien { get; set; }
        public IFormFile? AnhMonAn1URL { get; set; }
        public IFormFile? AnhMonAn2URL { get; set; }
        public IFormFile? AnhMonAn3URL { get; set; }
    }
}
