using DatBanNhaHang.Entities.NhaHang;
using DatBanNhaHang.Pagination;
using DatBanNhaHang.Payloads.Converters.NhaHang;
using DatBanNhaHang.Payloads.DTOs.NhaHang;
using DatBanNhaHang.Payloads.Requests.NhaHang.KhachHang;
using DatBanNhaHang.Payloads.Responses;
using DatBanNhaHang.Services.Implements.DatBanNhaHang.Service.Implements;
using DatBanNhaHang.Services.IServices;

namespace DatBanNhaHang.Services.Implements
{
    public class KhachHangServices : BaseService, IKhachHang
    {
        private readonly KhachHangConverters converters;
        private readonly ResponseObject<KhachHangDTOs> response;

        public KhachHangServices()
        {
            converters = new KhachHangConverters();
            response = new ResponseObject<KhachHangDTOs>();
        }
        public async Task<IQueryable<KhachHangDTOs>> HienThiKhachHang(Pagintation pagintation)
        {
            return contextDB.KhachHang.Select(x => converters.EntityToDTOs(x));
        }

        public async Task<ResponseObject<KhachHangDTOs>> SuaKhachHang(Request_SuaKhachHang request)
        {
            var kh = contextDB.KhachHang.SingleOrDefault(x => x.id == request.id);
            if (kh == null)
            {
                return response.ResponseError(StatusCodes.Status404NotFound, "Không tìm thấy khách hàng này ", null);
            }
            kh.HoTen = request.HoTen == null ? kh.HoTen : request.HoTen;
            kh.NgaySinh = request.NgaySinh == null ? kh.NgaySinh : request.NgaySinh;
            kh.DiaChi = request.DiaChi == null ? kh.DiaChi : request.DiaChi;
            kh.SDT = request.SDT == null ? kh.SDT : request.SDT;
            contextDB.Update(kh);
            await contextDB.SaveChangesAsync();
            return response.ResponseSuccess("Sửa khách hàng thành công ", converters.EntityToDTOs(kh));
        }

        public async Task<ResponseObject<KhachHangDTOs>> ThemKhachHang(Request_ThemKhachHang request)
        {
            if (string.IsNullOrWhiteSpace(request.HoTen)|| request.NgaySinh == null || string.IsNullOrWhiteSpace(request.DiaChi) && string.IsNullOrWhiteSpace(request.SDT))
            {
                return response.ResponseError(StatusCodes.Status404NotFound, "Không nhập đủ thông tin ", null);
            }
            KhachHang kh = new KhachHang()
            {
                HoTen = request.HoTen,
                DiaChi = request.DiaChi,
                NgaySinh = request.NgaySinh,
                SDT = request.SDT
            };
            await contextDB.AddAsync(kh);
            await contextDB.SaveChangesAsync();
            return response.ResponseSuccess("Thêm khách hàng thành công ", converters.EntityToDTOs(kh));
        }

        public async Task<IQueryable<KhachHangDTOs>> TimKiemKhachHang(Request_TimKiemKhachHang request, Pagintation pagintation)
        {
            if (request.HoTen != null && request.SDT != null)
            {
                var result = contextDB.KhachHang.Where(x => x.SDT == request.SDT);
                return result.Where(x => x.HoTen == request.HoTen).Select(y => converters.EntityToDTOs(y));
            }
            if (request.HoTen != null)
            {
                return contextDB.KhachHang.Where(x => x.HoTen == request.HoTen).Select(y => converters.EntityToDTOs(y));
            }
            if
                (request.SDT != null)
            {
                return contextDB.KhachHang.Where(x => x.SDT == request.SDT).Select(y => converters.EntityToDTOs(y));
            }
            
                return null;
             
        }

        public async Task<ResponseObject<KhachHangDTOs>> XoaKhachHang(Request_XoaKhachHang request)
        {
            var kh = contextDB.KhachHang.SingleOrDefault(x => x.id == request.id);
            if (kh == null)
            {
                return response.ResponseError(StatusCodes.Status404NotFound, "Không tìm thấy khách hàng này ", null);
            }
            //xoá các hoá đơn của khách hàng
            var lstHD = contextDB.HoaDon.Where(x => x.KhachHangID == kh.id).ToList();
            foreach (var x in lstHD)
            {
                var lstCTHD = contextDB.ChiTietHoaDon.Where(y => y.HoaDonID == x.id);
                contextDB.RemoveRange(lstCTHD);
            }
            contextDB.RemoveRange(lstHD);
            contextDB.Remove(kh);
            await contextDB.SaveChangesAsync();
            return response.ResponseSuccess("Xoá khách hàng thành công ", converters.EntityToDTOs(kh));

        }
    }
}
