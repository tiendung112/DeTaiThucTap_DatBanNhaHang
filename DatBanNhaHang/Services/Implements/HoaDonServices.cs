using DatBanNhaHang.Entities.NhaHang;
using DatBanNhaHang.Payloads.DTOs.NhaHang;
using DatBanNhaHang.Payloads.Requests.NhaHang.ChiTietHoaDon;
using DatBanNhaHang.Payloads.Requests.NhaHang.HoaDon;
using DatBanNhaHang.Payloads.Responses;
using DatBanNhaHang.Services.Implements.DatBanNhaHang.Service.Implements;
using DatBanNhaHang.Services.IServices;

namespace DatBanNhaHang.Services.Implements
{
    public class HoaDonServices : BaseService, IHoaDon
    {
        public Task<ResponseObject<HoaDonDTO>> ThemHoaDonUser(int khID, Request_ThemHoaDon_User request)
        {
            throw new NotImplementedException();
        }

        private List<Request_ThemChiTietHoaDon> ThemChiTietHoaDon(List<ChiTietHoaDon> lst) {
            throw new NotImplementedException();
        }
    }
}
