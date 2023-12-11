using DatBanNhaHang.Entities.NhaHang;
using DatBanNhaHang.Handler.Pagination;
using DatBanNhaHang.Payloads.DTOs.NguoiDung;
using DatBanNhaHang.Payloads.Requests.NguoiDung.User.LienHe;
using DatBanNhaHang.Payloads.Responses;

namespace DatBanNhaHang.Services.IServices
{
    public interface ILienHe
    {
        Task<ResponseObject<LienHeDTOs>> ThemLienHe(Request_ThemLienHe request);
        Task<ResponseObject<LienHeDTOs>> XacNhanLienHe(int LienHeId );
        Task<ResponseObject<LienHeDTOs>> XoaLienHe(int LienHeId);
        Task<List<LienHeDTOs>> XoaLienHeQuaLau();
        Task<PageResult<LienHeDTOs>>HienThiLienHe(int LienHeId,int pageSize, int pageNumber);
    }
}
