using System.ComponentModel.DataAnnotations;

namespace DatBanNhaHang.Payloads.Requests.NhaHang.DauBep
{
    public class Request_ThemDauBep
    {
        public string? HoTen { get; set; }
        [DataType(DataType.Upload)]
        public IFormFile? AnhDauBepURl { get; set; }

        public DateTime? ngaySinh { get; set; }
        public string? SDT { get; set; }
        public string? MoTa { get; set; }
    }
}
