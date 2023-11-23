using DatBanNhaHang.Handler.Pagination;
using DatBanNhaHang.Payloads.DTOs.NhaHang;
using DatBanNhaHang.Payloads.Requests.NhaHang.LoaiMonAn;
using DatBanNhaHang.Payloads.Responses;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Numerics;

namespace DatBanNhaHang.Services.IServices
{
    public interface ILoaiMonAn
    {
        Task<ResponseObject<LoaiMonAnDTOs>> ThemLoaiMonAn(Request_ThemLoaiMonAn request);
        Task<ResponseObject<LoaiMonAnDTOs>> ThemLoaiMonAnKemMonAn(Request_ThemLoaiMonAnKemMonAn request);
        Task<ResponseObject<LoaiMonAnDTOs>> SuaLoaiMonAn(int id,Request_SuaLoaiMonAn request);
        Task<ResponseObject<LoaiMonAnDTOs>> XoaLoaiMonAn(int id);
        Task<PageResult<LoaiMonAnDTOs>> HienThiLoaiMonAn (int id ,int pageSize, int pageNumber);

    }
}
