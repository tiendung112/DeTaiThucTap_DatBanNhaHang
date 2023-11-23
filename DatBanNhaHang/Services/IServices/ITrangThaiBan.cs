using DatBanNhaHang.Payloads.DTOs.NhaHang;
using DatBanNhaHang.Payloads.Requests.NhaHang.TrangThaiBan;
using DatBanNhaHang.Payloads.Responses;
using Microsoft.EntityFrameworkCore.Query;

namespace DatBanNhaHang.Services.IServices
{
    public interface ITrangThaiBan
    {
        Task<ResponseObject<TrangThaiBanDTOs>> ThemTrangThaiBan(Request_ThemTrangThaiBan request);
        Task<ResponseObject<TrangThaiBanDTOs>> SuaTrangThaiBan(Request_SuaTrangThaiBan request);
        Task<ResponseObject<TrangThaiBanDTOs>> XoaTrangThaiBan(Request_XoaTrangThaiBan request);
        Task<IQueryable<TrangThaiBanDTOs>> HienThiTrangThaiBan(int pageSize, int pageNumber);
    }
}
