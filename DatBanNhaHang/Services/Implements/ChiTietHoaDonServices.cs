using DatBanNhaHang.Payloads.Converters.NhaHang;
using DatBanNhaHang.Payloads.DTOs.NhaHang;
using DatBanNhaHang.Payloads.Responses;
using DatBanNhaHang.Services.Implements.DatBanNhaHang.Service.Implements;
using DatBanNhaHang.Services.IServices;

namespace DatBanNhaHang.Services.Implements
{

    public class ChiTietHoaDonServices : BaseService, IChiTietHoaDon
    {
        private readonly ResponseObject<ChiTietHoaDonDTOs> response;
        private readonly ChiTietHoaDonConverters converters;
        public ChiTietHoaDonServices()
        {
            response = new ResponseObject<ChiTietHoaDonDTOs>();
            converters = new ChiTietHoaDonConverters();
        }
        /* public async Task<IQueryable<ChiTietHoaDonDTOs>> HienThiChiTietHoaDon(int pageSize, int pageNumber)
         {
             return contextDB.ChiTietHoaDon.Select(y => converters.EntityToDTOs(y));
         }

        *//* public async Task<IQueryable<ChiTietHoaDonDTOs>> TimKiemThiChiTietHoaDonTheoHoaDon(Request_TimKiemChiTietHoaDonTheoHoaDon request)
         {
             return contextDB.ChiTietHoaDon.Where(x => x.HoaDonID == request.HoaDonID).Select(y=>converters.EntityToDTOs(y));
         }
 *//*
         public async Task<ResponseObject<ChiTietHoaDonDTOs>> SuaChiTietHoaDon(Request_SuaChiTietHoaDon request)
         {
             throw new NotImplementedException();
         }

         public async Task<ResponseObject<ChiTietHoaDonDTOs>> ThemChiTietHoaDon(Request_ThemChiTietHoaDon request)
         {
            *//* if (!contextDB.HoaDon.Any(x => x.id == request.HoaDonID))
             {
                 return response.ResponseError(StatusCodes.Status404NotFound, "Không tồn tại hoá đơn này", null);
             }
             if (!contextDB.MonAn.Any(x => x.id == request.MonAnID))
             {
                 return response.ResponseError(StatusCodes.Status404NotFound, "Không tồn tại món này", null);
             }
             if (string.IsNullOrWhiteSpace(request.DonViTinh) || request.SoLuong <= 0)
             {
                 return response.ResponseError(StatusCodes.Status404NotFound, "Chưa đủ thông tin ", null);
             }
             var monan = contextDB.MonAn.SingleOrDefault(x=>x.id==request.MonAnID);
             ChiTietHoaDon chiTietHoaDon = new ChiTietHoaDon()
             {
                 MonAnID = request.MonAnID,
                 DonViTinh = request.DonViTinh,
                 HoaDonID = request.HoaDonID,
                 SoLuong = request.SoLuong,
                 ThanhTien =request.SoLuong * monan.GiaTien
             };
             contextDB.ChiTietHoaDon.Add(chiTietHoaDon);
             await contextDB.SaveChangesAsync();*//*
             return response.ResponseSuccess("Thêm thành công chi tiết hoá đơn ",*//*converters.EntityToDTOs(chiTietHoaDon)*//*null);
         }

         public async Task<ResponseObject<ChiTietHoaDonDTOs>> XoaChiTietHoaDon(Request_XoaChiTietHoaDon request)
         {
             throw new NotImplementedException();
         }*/
    }
}
