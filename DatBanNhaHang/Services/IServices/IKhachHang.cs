using DatBanNhaHang.Handler.Pagination;
using DatBanNhaHang.Payloads.DTOs.NhaHang;
using DatBanNhaHang.Payloads.Requests.NhaHang.KhachHang;
using DatBanNhaHang.Payloads.Responses;

namespace DatBanNhaHang.Services.IServices
{
    public interface IKhachHang
    {
        Task<ResponseObject<KhachHangDTOs>> ThemKhachHang(Request_ThemKhachHang request);
        Task<ResponseObject<KhachHangDTOs>> SuaKhachHang(Request_SuaKhachHang request);
        Task<ResponseObject<KhachHangDTOs>> XoaKhachHang(Request_XoaKhachHang request);
        Task<IQueryable<KhachHangDTOs>> TimKiemKhachHang(Request_TimKiemKhachHang request,Pagintation pagintation);
        Task<IQueryable<KhachHangDTOs>> HienThiKhachHang(int pageSize, int pageNumber);
    }
}
