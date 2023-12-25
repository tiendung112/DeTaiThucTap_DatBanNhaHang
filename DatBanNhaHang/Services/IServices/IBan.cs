using DatBanNhaHang.Handler.Pagination;
using DatBanNhaHang.Payloads.DTOs.NhaHang;
using DatBanNhaHang.Payloads.Requests.NhaHang.Ban;
using DatBanNhaHang.Payloads.Responses;

namespace DatBanNhaHang.Services.IServices
{
    public interface IBan
    {
        Task<ResponseObject<BanDTOs>> ThemBan(Request_ThemBan request);
        Task<ResponseObject<BanDTOs>> SuaBan(int id, Request_SuaBan request);
        Task<ResponseObject<BanDTOs>> XoaBan(int id);
        Task<PageResult<BanDTOs>> HienThiBan(int id, int pageSize, int pageNumber);

        Task<PageResult<BanDTOs>> HienThiBanTheoViTri(int pageSize, int pageNumber);
        Task<PageResult<BanDTOs>> HienThiBanTheoLoaiBan(int lbid, int pageSize, int pageNumber);

    }
}
