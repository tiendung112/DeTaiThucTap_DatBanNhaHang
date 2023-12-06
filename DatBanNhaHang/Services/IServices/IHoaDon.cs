using DatBanNhaHang.Payloads.DTOs.NhaHang;
using DatBanNhaHang.Payloads.Requests.NhaHang.HoaDon;
using DatBanNhaHang.Payloads.Responses;

namespace DatBanNhaHang.Services.IServices
{
    public interface IHoaDon
    {
        public Task<string> ThemHoaDonUser(int userid ,Request_ThemHoaDon_User request);
        public Task<ResponseObject<HoaDonDTO>> SuaHoaDon();
    }
}
