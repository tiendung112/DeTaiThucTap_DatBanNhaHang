using DatBanNhaHang.Pagination;
using DatBanNhaHang.Payloads.DTOs.NhaHang;
using DatBanNhaHang.Payloads.Requests.NhaHang.Ban;
using DatBanNhaHang.Payloads.Responses;

namespace DatBanNhaHang.Services.IServices
{
    public interface IBan
    {
        Task<ResponseObject<BanDTOs>> ThemBan(Request_ThemBan request);
        Task<ResponseObject<BanDTOs>> SuaBan(Request_SuaBan request);
        Task<ResponseObject<BanDTOs>> XoaBan(Request_XoaBan request);
        Task<IQueryable<BanDTOs>> TimkiemBan(Request_TimKiemBan request);
        Task<IQueryable<BanDTOs>> HienThiBan(Pagintation pagintation);
        Task<IQueryable<BanDTOs>> HienThiBanTheoTrangThai(int ttID, Pagintation pagintation);
        Task<IQueryable<BanDTOs>> HienThiBanTheoViTri(Pagintation pagintation);
        Task<IQueryable<BanDTOs>> HienThiBanTheoLoaiBan(int LB, Pagintation pagintation);
    }
}
