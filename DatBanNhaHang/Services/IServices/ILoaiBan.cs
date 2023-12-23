using DatBanNhaHang.Handler.Pagination;
using DatBanNhaHang.Payloads.DTOs.NhaHang;
using DatBanNhaHang.Payloads.Requests.NhaHang.LoaiBan;
using DatBanNhaHang.Payloads.Responses;

namespace DatBanNhaHang.Services.IServices
{
    public interface ILoaiBan
    {
        Task<ResponseObject<LoaiBanDTOs>> ThemLoaiBan(Request_ThemLoaiBan request);
        Task<ResponseObject<LoaiBanDTOs>> SuaLoaiBan(int id,Request_SuaLoaiBan request);
        Task<ResponseObject<LoaiBanDTOs>> XoaLoaiBan(int id );
        Task<PageResult<SingleLoaiBanDTOs>> HienThiLoaiBan(int id,int pageSize, int pageNumber);
        Task<PageResult<LoaiBanDTOs>> HienThiLoaiBanKemBan(int id, int pageSize, int pageNumber);
    }
}
