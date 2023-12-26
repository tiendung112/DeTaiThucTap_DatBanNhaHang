using DatBanNhaHang.Handler.Pagination;
using DatBanNhaHang.Payloads.DTOs.NhaHang;
using DatBanNhaHang.Payloads.Requests.NguoiDung.User;
using DatBanNhaHang.Payloads.Requests.NhaHang.HoaDon;
using DatBanNhaHang.Payloads.Responses;

namespace DatBanNhaHang.Services.IServices
{
    public interface IHoaDon
    {
        Task<ResponseObject<HoaDonDTO>> ThemHoaDonUser(int userid, Request_ThemHoaDon_User request);
        Task<ResponseObject<HoaDonDTO>> SuaHoaDon(int userid, int hoadonid, Request_SuaHoaDon_User request);
        Task<ResponseObject<HoaDonDTO>> HuyHoaDon(int hoadonid, int userid);
        //Task<string> XacNhanOrder(int id, int hoadonid, Request_ValidateRegister request);
        Task<string> XacNhanHuyOrder(int id, int hoadonid, Request_ValidateRegister request);
        Task<ResponseObject<List<BanDTOs>>> TimBanTrong(Request_timBanTrong request);
        Task<ResponseObject<List<BanDTOs>>> HienThiBanTrong();
        Task<ResponseObject<HoaDonDTO>> CapNhatThongTinHoaDon(int id );
        //Task<ResponseObject<HoaDonDTO>> ThemHoaDonAdmin(Request_ThemHoaDon_Admin request);
        Task<ResponseObject<HoaDonDTO>> XoaHoaDonAdmin(int HoaDonid);
        Task<ResponseObject<HoaDonDTO>> SuaHoaDonAdmin(int hoaDonid, Request_SuaHoaDon request);
        Task<string> XoaTatCaHoaDonChuaDuyet();
        Task<PageResult<HoaDonDTO>> HienThiHoaDon(int hoadonid, int pageSize, int pageNumber);
        Task<PageResult<HoaDonDTO>> HienThiHoaDonCuaUser(int userid, int pageSize, int pageNumber);
        //Task<PageResult<HoaDonDTO>> HienThiHoaDonCuaKhachHang(int khid, int pageSize, int pageNumber);
    }
}
