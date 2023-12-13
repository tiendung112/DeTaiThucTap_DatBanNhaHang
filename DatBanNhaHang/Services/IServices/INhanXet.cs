using DatBanNhaHang.Handler.Pagination;
using DatBanNhaHang.Payloads.DTOs.NguoiDung;
using DatBanNhaHang.Payloads.Requests.NguoiDung.Admin.NhanXet;
using DatBanNhaHang.Payloads.Responses;
using DatBanNhaHang.Services.Implements.DatBanNhaHang.Service.Implements;

namespace DatBanNhaHang.Services.IServices
{
    public interface INhanXet 
    {
        Task<ResponseObject<NhanXetDTOs>> ThemNhanXet(Request_ThemNhanXet request);
        Task<ResponseObject<NhanXetDTOs>> SuaNhanXet(int nhanxetid, Request_SuaNhanXet request);
        Task<ResponseObject<NhanXetDTOs>> XoaNhanXet(int nhanxetid);
        Task<PageResult<NhanXetDTOs>> HienThiNhanXet(int nhanxetid, int pageSize, int pageNumber);
    }
}
