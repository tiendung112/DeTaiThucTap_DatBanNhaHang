namespace DatBanNhaHang.Payloads.Requests.NhaHang.DauBep
{
    public class Request_SuaDauBep
    {
        public int ID { get; set; }
        public string? HoTen { get; set; }

        public IFormFile? AnhDauBepURl { get; set; }

        public DateTime? ngaySinh { get; set; }
        public string? SDT { get; set; }
        public string? MoTa { get; set; }
    }
}
