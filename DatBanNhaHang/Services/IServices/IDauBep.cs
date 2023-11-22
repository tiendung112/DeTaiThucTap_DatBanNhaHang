﻿using DatBanNhaHang.Pagination;
using DatBanNhaHang.Payloads.DTOs.NhaHang;
using DatBanNhaHang.Payloads.Requests.NhaHang.DauBep;
using DatBanNhaHang.Payloads.Responses;

namespace DatBanNhaHang.Services.IServices
{
    public interface IDauBep
    {
        Task<ResponseObject<DauBepDTOs>> ThemDauBep (Request_ThemDauBep  request);
        Task<ResponseObject<DauBepDTOs>> SuaDauBep(Request_SuaDauBep request);
        Task<ResponseObject<DauBepDTOs>> XoaDauBep(Request_XoaDauBep request);
        Task<IQueryable<DauBepDTOs>> GetDSDauBep(Pagintation pagintation);
    }
}
