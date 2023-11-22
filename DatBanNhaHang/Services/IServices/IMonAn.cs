﻿using DatBanNhaHang.Pagination;
using DatBanNhaHang.Payloads.DTOs.NhaHang;
using DatBanNhaHang.Payloads.Requests.NhaHang.MonAn;
using DatBanNhaHang.Payloads.Responses;
using DatBanNhaHang.Services.Implements.DatBanNhaHang.Service.Implements;

namespace DatBanNhaHang.Services.IServices
{
    public interface IMonAn 
    {
        Task<ResponseObject<MonAnDTOs>> ThemMonAn(Request_ThemMonAn request);
        Task<ResponseObject<MonAnDTOs>> SuaMonAn(Request_SuaMonAn request);
        Task<ResponseObject<MonAnDTOs>> XoaMonAn(Request_XoaMonAn request);
        Task<IQueryable<MonAnDTOs>> TimKiemMonAn(Request_TimKiemMonAn request,Pagintation pagintation);
        Task<IQueryable<MonAnDTOs>> HienThiMonAn(Pagintation pagintation);
    }
}