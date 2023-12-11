using DatBanNhaHang.Handler.Pagination;
using DatBanNhaHang.Payloads.DTOs.NhaHang;
using DatBanNhaHang.Payloads.Requests.NhaHang.KhachHang;
using DatBanNhaHang.Payloads.Responses;

namespace DatBanNhaHang.Services.IServices
{
    public interface IKhachHang
    {
        Task<ResponseObject<KhachHangDTOs>> ThemKhachHang(Request_ThemKhachHang request);
        Task<ResponseObject<KhachHangDTOs>> SuaKhachHang(int id , Request_SuaKhachHang request);
        Task<ResponseObject<KhachHangDTOs>> NangCapThongTinKhachHangACC(Request_NangCapThongTinKhachHang request);
        Task<ResponseObject<KhachHangDTOs>> XoaKhachHang( int id );
        //Task<PageResult<KhachHangDTOs>> TimKiemKhachHangSDT(string SDT);
        //Task<PageResult<KhachHangDTOs>> TimKiemKhachHangHoTen(string HoTen, int pageSize, int pageNumber);
        Task<PageResult<KhachHangDTOs>> HienThiKhachHang(int id,int pageSize, int pageNumber);
    }
}
