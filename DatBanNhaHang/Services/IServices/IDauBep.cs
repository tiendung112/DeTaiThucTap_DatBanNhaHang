using DatBanNhaHang.Handler.Pagination;
using DatBanNhaHang.Payloads.DTOs.NhaHang;
using DatBanNhaHang.Payloads.Requests.NhaHang.DauBep;
using DatBanNhaHang.Payloads.Responses;

namespace DatBanNhaHang.Services.IServices
{
    public interface IDauBep
    {
        Task<ResponseObject<DauBepDTOs>> ThemDauBep (Request_ThemDauBep  request);
        Task<ResponseObject<DauBepDTOs>> SuaDauBep(int id,Request_SuaDauBep request);
        Task<ResponseObject<DauBepDTOs>> XoaDauBep(int id);
        Task<PageResult<DauBepDTOs>> GetDSDauBep(int id , int pageSize, int pageNumber);

    }
}
