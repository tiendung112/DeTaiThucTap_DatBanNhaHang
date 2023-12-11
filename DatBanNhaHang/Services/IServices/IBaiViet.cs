using DatBanNhaHang.Handler.Pagination;
using DatBanNhaHang.Payloads.DTOs.NguoiDung;
using DatBanNhaHang.Payloads.Requests.NguoiDung.Admin.Blog;
using DatBanNhaHang.Payloads.Responses;

namespace DatBanNhaHang.Services.IServices
{
    public interface IBaiViet
    {
        Task<ResponseObject<BaiVietDTOs>> ThemBaiViet(int adminid,Request_ThemBaiViet request);
        Task<ResponseObject<BaiVietDTOs>> SuaBaiViet(int blogid, int adminid, Request_SuaBaiViet request);
        Task<ResponseObject<BaiVietDTOs>> XoaBaiViet(int blogid);
        Task<PageResult<BaiVietDTOs>> HienThiBaiViet(int blogid,int  pageSize, int pageNumber);
    }
}
