using DatBanNhaHang.Payloads.DTOs.NhaHang;
using DatBanNhaHang.Payloads.Requests.NhaHang.TrangThaiHoaDon;
using DatBanNhaHang.Payloads.Responses;

namespace DatBanNhaHang.Services.IServices
{
    public interface ITrangThaiHoaDon
    {
        Task<ResponseObject<TrangThaiHoaDonDTOs>> ThemTrangThaiHoaDon(Request_ThemTrangThaiHoaDon request);
        Task<ResponseObject<TrangThaiHoaDonDTOs>> SuaTrangThaiHoaDon(Request_SuaTrangThaiHoaDon request);
        Task<ResponseObject<TrangThaiHoaDonDTOs>> XoaTrangThaiHoaDon(Request_XoaTrangThaiHoaDon request);
        Task<IQueryable<TrangThaiHoaDonDTOs>> HienThiTrangThaiHoaDon(int pageSize, int pageNumber);


    }
}
