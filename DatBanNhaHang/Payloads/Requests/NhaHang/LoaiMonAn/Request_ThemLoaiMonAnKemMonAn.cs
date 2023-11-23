using DatBanNhaHang.Payloads.Requests.NhaHang.MonAn;

namespace DatBanNhaHang.Payloads.Requests.NhaHang.LoaiMonAn
{
    public class Request_ThemLoaiMonAnKemMonAn
    {
        public string tenLoaiMonAn { get; set; }
        public IEnumerable<Request_ThemMonAn>? MonAn { get; set; }
    }
}
