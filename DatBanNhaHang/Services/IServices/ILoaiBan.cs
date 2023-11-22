using DatBanNhaHang.Payloads.DTOs.NhaHang;
using DatBanNhaHang.Payloads.Requests.NhaHang.LoaiBan;
using DatBanNhaHang.Payloads.Responses;

namespace DatBanNhaHang.Services.IServices
{
    public interface ILoaiBan
    {
        Task<ResponseObject<LoaiBanDTOs>> ThemLoaiBan(Request_ThemLoaiBan request);
        Task<ResponseObject<LoaiBanDTOs>> SuaLoaiBan(Request_SuaLoaiBan request);
        Task<ResponseObject<LoaiBanDTOs>> XoaLoaiBan(Request_XoaLoaiBan request);
        Task<IQueryable<LoaiBanDTOs>> HienThiLoaiBan();

    }
}
