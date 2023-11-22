using DatBanNhaHang.Pagination;
using DatBanNhaHang.Payloads.DTOs.NhaHang;
using DatBanNhaHang.Payloads.Requests.NhaHang.LoaiMonAn;
using DatBanNhaHang.Payloads.Responses;
using System.Numerics;

namespace DatBanNhaHang.Services.IServices
{
    public interface ILoaiMonAn
    {
        Task<ResponseObject<LoaiMonAnDTOs>> ThemLoaiMonAn(Request_ThemLoaiMonAn request);
        Task<ResponseObject<LoaiMonAnDTOs>> ThemLoaiMonAnKemMonAn(Request_ThemLoaiMonAn request);
        Task<ResponseObject<LoaiMonAnDTOs>> SuaLoaiMonAn(Request_SuaLoaiMonAn request);
        Task<ResponseObject<LoaiMonAnDTOs>> XoaLoaiMonAn(Request_XoaLoaiMonAn request);
        Task<IQueryable<LoaiMonAnDTOs>> HienThiLoaiMonAn (Pagintation pagintation);

    }
}
