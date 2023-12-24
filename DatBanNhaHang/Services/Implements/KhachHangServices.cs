using DatBanNhaHang.Entities.NhaHang;
using DatBanNhaHang.Handler.Pagination;
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
        public async Task<PageResult<KhachHangDTOs>> HienThiKhachHang(int id, int pageSize, int pageNumber)
        {
            var kh = id == 0 ? contextDB.KhachHang.Select(x => converters.EntityToDTOs(x)) : contextDB.KhachHang.Where(y => y.id == id).Select(x => converters.EntityToDTOs(x));
            var result = Pagintation.GetPagedData(kh, pageSize, pageNumber);
            return result;
        }

        public async Task<ResponseObject<KhachHangDTOs>> SuaKhachHang(int id, Request_SuaKhachHang request)
        {
            var kh = contextDB.KhachHang.SingleOrDefault(x => x.id == id);
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
        public async Task<ResponseObject<KhachHangDTOs>> NangCapThongTinKhachHangACC(Request_NangCapThongTinKhachHang request)
        {
            var user = contextDB.User.SingleOrDefault(x => x.id == request.UserId);
            if (user == null)
            {
                return response.ResponseError(StatusCodes.Status404NotFound, "Không tồn tại tài khoản này ", null);
            }
            var khachhang = contextDB.KhachHang.SingleOrDefault(x => x.userID == request.UserId);
            if (khachhang == null)
            {
                KhachHang kh = new KhachHang()
                {
                    DiaChi = user.address,
                    HoTen = user.Name,
                    NgaySinh = user.DateOfBirth,
                    SDT = user.SDT,
                    userID = user.id,
                };
                await contextDB.AddAsync(kh);
                await contextDB.SaveChangesAsync();
                return response.ResponseSuccess("Sửa khách hàng thành công ", converters.EntityToDTOs(kh));
            }
            khachhang.DiaChi = user.address;
            khachhang.HoTen = user.Name;
            khachhang.NgaySinh = user.DateOfBirth;
            khachhang.SDT = user.SDT;

            contextDB.Update(khachhang);
            await contextDB.SaveChangesAsync();
            return response.ResponseSuccess("Thêm khách hàng thành công ", converters.EntityToDTOs(khachhang));
        }


        public async Task<ResponseObject<KhachHangDTOs>> ThemKhachHang(Request_ThemKhachHang request)
        {
            if (contextDB.User.Any(x => x.id == request.userID))
            {
                var user = contextDB.User.SingleOrDefault(x => x.id == request.userID);
                if (user == null)
                {
                    return response.ResponseError(StatusCodes.Status404NotFound, "Không tồn tại tài khoản này ", null);
                }
                KhachHang khachhang = new KhachHang()
                {
                    DiaChi = user.address,
                    HoTen = user.Name,
                    NgaySinh = user.DateOfBirth,
                    SDT = user.SDT,
                    userID = user.id,
                };
                await contextDB.AddAsync(khachhang);
                await contextDB.SaveChangesAsync();
                return response.ResponseSuccess("Thêm khách hàng thành công ", converters.EntityToDTOs(khachhang));
            }
            if (string.IsNullOrWhiteSpace(request.HoTen) || string.IsNullOrWhiteSpace(request.DiaChi) && string.IsNullOrWhiteSpace(request.SDT))
            {
                return response.ResponseError(StatusCodes.Status404NotFound, "Không nhập đủ thông tin ", null);
            }
            KhachHang kh = new KhachHang()
            {
                HoTen = request.HoTen,
                DiaChi = request.DiaChi,
                NgaySinh = request.NgaySinh,
                SDT = request.SDT,
            };
            await contextDB.AddAsync(kh);
            await contextDB.SaveChangesAsync();
            return response.ResponseSuccess("Thêm khách hàng thành công ", converters.EntityToDTOs(kh));
        }

        public async Task<PageResult<KhachHangDTOs>> TimKiemKhachHangSDT(Request_TimSDT request)
        {

            var kh = contextDB.KhachHang.Where(x => x.SDT == request.SDT).Select(y => converters.EntityToDTOs(y));
            var result = Pagintation.GetPagedData(kh, 0, 0);
            return result;
        }

        //public async Task<PageResult<KhachHangDTOs>> TimKiemKhachHangHoTen(string HoTen, int pageSize, int pageNumber)
        //{
        //    var kh = contextDB.KhachHang.AsEnumerable()
        //        .Where(x => ChuanHoaChuoi(x.HoTen).Contains(ChuanHoaChuoi(HoTen))).AsQueryable()
        //        .Select(y => converters.EntityToDTOs(y));

        //    var result = Pagintation.GetPagedData<KhachHangDTOs>(kh, pageSize, pageNumber);
        //    return result;
        //}

        public async Task<ResponseObject<KhachHangDTOs>> XoaKhachHang(int id)
        {
            var kh = contextDB.KhachHang.SingleOrDefault(x => x.id == id);
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
