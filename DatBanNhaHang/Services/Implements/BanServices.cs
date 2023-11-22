using DatBanNhaHang.Entities.NhaHang;
using DatBanNhaHang.Pagination;
using DatBanNhaHang.Payloads.Converters.NhaHang;
using DatBanNhaHang.Payloads.DTOs.NhaHang;
using DatBanNhaHang.Payloads.Requests.NhaHang.Ban;
using DatBanNhaHang.Payloads.Responses;
using DatBanNhaHang.Services.Implements.DatBanNhaHang.Service.Implements;
using DatBanNhaHang.Services.IServices;

namespace DatBanNhaHang.Services.Implements
{
    public class BanServices : BaseService, IBan
    {
        private readonly BanConverters converters;
        private readonly ResponseObject<BanDTOs> response;

        public BanServices()
        {
            converters = new BanConverters();
            response = new ResponseObject<BanDTOs>();
        }
        public async Task<IQueryable<BanDTOs>> HienThiBan(Pagintation pagintation)
        {
            return contextDB.Ban.Select(x => converters.EntityToDTOs(x));
        }

        public async Task<IQueryable<BanDTOs>> HienThiBanTheoLoaiBan(int LB, Pagintation pagintation)
        {
            return contextDB.Ban.Where(x => x.LoaiBanID == LB).Select(y => converters.EntityToDTOs(y));
        }

        public async Task<IQueryable<BanDTOs>> HienThiBanTheoTrangThai(int ttId, Pagintation pagintation)
        {
            return contextDB.Ban.OrderBy(y => y.TrangThaiBanID).Select(x => converters.EntityToDTOs(x));
        }

        public async Task<IQueryable<BanDTOs>> HienThiBanTheoViTri(Pagintation pagintation)
        {
            return contextDB.Ban.OrderBy(y => y.ViTri).Select(x => converters.EntityToDTOs(x));
        }

        public async Task<ResponseObject<BanDTOs>> SuaBan(Request_SuaBan request)
        {
            if (!contextDB.Ban.Any(x => x.LoaiBanID == request.LoaiBanID))
            {
                return response.ResponseError(StatusCodes.Status404NotFound, "Không tồn tại loại bàn này", null);
            }
            if (!contextDB.Ban.Any(x => x.TrangThaiBanID == request.TrangThaiBanID))
            {
                return response.ResponseError(StatusCodes.Status404NotFound, "Không tồn tại trạng thái bàn này", null);
            }
            var ban = contextDB.Ban.SingleOrDefault(x => x.id == request.BanID);
            if (ban == null)
            {
                return response.ResponseError(StatusCodes.Status404NotFound, "Không có bàn cần sửa", null);
            }
            ban.ViTri = request.ViTri == null ? ban.ViTri : request.ViTri;
            ban.SoBan = !request.SoBan.HasValue ? ban.SoBan : request.SoBan;
            ban.SoNguoiToiDa = !request.SoNguoiToiDa.HasValue ? ban.SoNguoiToiDa : request.SoNguoiToiDa;
            ban.GiaTien =request.GiaTien.HasValue?request.GiaTien:ban.GiaTien;
            ban.LoaiBanID = !request.LoaiBanID.HasValue ? ban.LoaiBanID : request.LoaiBanID;
            ban.TrangThaiBanID = !request.TrangThaiBanID.HasValue?ban.TrangThaiBanID :request.TrangThaiBanID;
            contextDB.Update(ban);
            await contextDB.SaveChangesAsync();
            return response.ResponseSuccess("Sửa bàn thành công", converters.EntityToDTOs(ban));

        }

        public async Task<ResponseObject<BanDTOs>> ThemBan(Request_ThemBan request)
        {
            if (string.IsNullOrWhiteSpace(request.ViTri) || !request.GiaTien.HasValue || !request.SoBan.HasValue
                || !request.SoNguoiToiDa.HasValue || !request.GiaTien.HasValue || !request.LoaiBanID.HasValue || !request.TrangThaiBanID.HasValue)
            {
                return response.ResponseError(StatusCodes.Status404NotFound, "chưa điền đầy đủ thông tin ", null);
            }
            if (!contextDB.TrangThaiBan.Any(x => x.id == request.TrangThaiBanID))
            {
                return response.ResponseError(StatusCodes.Status404NotFound, "Không tồn tại trạng thái bàn này", null);
            }
            if (!contextDB.LoaiBan.Any(x => x.id == request.LoaiBanID))
            {
                return response.ResponseError(StatusCodes.Status404NotFound, "Không tồn tại loại bàn này", null);
            }
            Ban newBan = new Ban()
            {
                SoBan = request.SoBan,
                ViTri = request.ViTri,
                GiaTien = request.GiaTien,
                LoaiBanID = request.LoaiBanID,
                SoNguoiToiDa = request.SoNguoiToiDa,
                TrangThaiBanID = request.TrangThaiBanID
            };
            contextDB.Ban.Add(newBan);
            await contextDB.SaveChangesAsync();
            return response.ResponseSuccess("thêm bàn thành công", converters.EntityToDTOs(newBan));
        }

        public async Task<IQueryable<BanDTOs>> TimkiemBan(Request_TimKiemBan request)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseObject<BanDTOs>> XoaBan(Request_XoaBan request)
        {
            var ban = contextDB.Ban.SingleOrDefault(x => x.id == request.BanID);
            if (ban == null)
            {
                return response.ResponseError(StatusCodes.Status404NotFound, "Không có bàn cần Xoá", null);
            }
            var lstHD = contextDB.HoaDon.Where(x=>x.BanID == ban.id).ToList();
            foreach(var x in lstHD)
            {
                var lstCTHD = contextDB.ChiTietHoaDon.Where(x => x.HoaDonID == x.id);
                contextDB.ChiTietHoaDon.RemoveRange(lstCTHD);
            }
            contextDB.HoaDon.RemoveRange(lstHD);
            contextDB.Ban.Remove(ban);
            return response.ResponseSuccess("Xoá bàn thành công", converters.EntityToDTOs(ban));
        }
    }
}
