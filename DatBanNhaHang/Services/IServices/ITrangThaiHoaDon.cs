using DatBanNhaHang.Handler.Pagination;
using DatBanNhaHang.Payloads.DTOs.NhaHang;
using DatBanNhaHang.Payloads.Requests.NhaHang.TrangThaiHoaDon;
using DatBanNhaHang.Payloads.Responses;

namespace DatBanNhaHang.Services.IServices
{
    public interface ITrangThaiHoaDon
    {
        Task<ResponseObject<TrangThaiHoaDonDTOs>> ThemTrangThaiHoaDon(Request_ThemTrangThaiHoaDon request);
        Task<ResponseObject<TrangThaiHoaDonDTOs>> SuaTrangThaiHoaDon(int id , Request_SuaTrangThaiHoaDon request);
        Task<ResponseObject<TrangThaiHoaDonDTOs>> XoaTrangThaiHoaDon(int id );
        Task<PageResult<TrangThaiHoaDonDTOs>> HienThiTrangThaiHoaDon(int id, int pageSize, int pageNumber);


    }
}
