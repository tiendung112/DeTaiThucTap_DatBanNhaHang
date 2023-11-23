using DatBanNhaHang.Handler.Pagination;
using DatBanNhaHang.Payloads.DTOs.NhaHang;
using DatBanNhaHang.Payloads.Requests.NhaHang.MonAn;
using DatBanNhaHang.Payloads.Responses;
using DatBanNhaHang.Services.Implements.DatBanNhaHang.Service.Implements;

namespace DatBanNhaHang.Services.IServices
{
    public interface IMonAn 
    {
        Task<ResponseObject<MonAnDTOs>> ThemMonAn(Request_ThemMonAn request);
        Task<ResponseObject<MonAnDTOs>> SuaMonAn(int id ,Request_SuaMonAn request);
        Task<ResponseObject<MonAnDTOs>> XoaMonAn(int id);
        Task<PageResult<MonAnDTOs>> TimKiemMonAn(string tenMonAn , int pageSize, int pageNumber );
        Task<PageResult<MonAnDTOs>> HienThiMonAn(int id , int pageSize, int pageNumber );
    }
}
