using DatBanNhaHang.Entities.NhaHang;
using DatBanNhaHang.Handler.Pagination;
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
        #region hiển thị và tìm kiếm bàn
        public async Task<PageResult<BanDTOs>> HienThiBan(int id, int pageSize, int pageNumber)
        {
            var ban = id == 0 ? contextDB.Ban.Select(x => converters.EntityToDTOs(x)) 
                : contextDB.Ban.Where(y => y.id == id).Select(x => converters.EntityToDTOs(x));
            var result = Pagintation.GetPagedData(ban, pageSize, pageNumber);
            return result;
        }

        public async Task<PageResult<BanDTOs>> HienThiBanTheoLoaiBan(int lbid, int pageSize, int pageNumber)
        {
            var ban = contextDB.Ban.Where(x => x.LoaiBanID == lbid).Select(y => converters.EntityToDTOs(y));
            var result = Pagintation.GetPagedData(ban, pageSize, pageNumber);
            return result;
        }
        public async Task<PageResult<BanDTOs>> HienThiBanTheoViTri(int pageSize, int pageNumber)
        {
            var lst = contextDB.Ban.OrderBy(y => y.ViTri).Select(x => converters.EntityToDTOs(x));
            var result = Pagintation.GetPagedData(lst, pageSize, pageNumber);
            return result;
        }
        public async Task<PageResult<BanDTOs>> TimkiemBan(string tenBan, int pageSize, int pageNumber)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region thêm sửa xoá bàn
        public async Task<ResponseObject<BanDTOs>> ThemBan(Request_ThemBan request)
        {
            if (string.IsNullOrWhiteSpace(request.ViTri) || !request.GiaTien.HasValue || !request.SoBan.HasValue
                || !request.SoNguoiToiDa.HasValue || !request.GiaTien.HasValue || !request.LoaiBanID.HasValue || !request.TrangThaiBanID.HasValue)
            {
                return response.ResponseError(StatusCodes.Status404NotFound, "chưa điền đầy đủ thông tin ", null);
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
                
            };
            contextDB.Ban.Add(newBan);
            await contextDB.SaveChangesAsync();
            return response.ResponseSuccess("thêm bàn thành công", converters.EntityToDTOs(newBan));
        }
        public async Task<ResponseObject<BanDTOs>> SuaBan(int id, Request_SuaBan request)
        {
            if (!contextDB.Ban.Any(x => x.LoaiBanID == request.LoaiBanID))
            {
                return response.ResponseError(StatusCodes.Status404NotFound, "Không tồn tại loại bàn này", null);
            }
           
            var ban = contextDB.Ban.SingleOrDefault(x => x.id == id);
            if (ban == null)
            {
                return response.ResponseError(StatusCodes.Status404NotFound, "Không có bàn cần sửa", null);
            }
            ban.ViTri = request.ViTri == null ? ban.ViTri : request.ViTri;
            ban.SoBan = !request.SoBan.HasValue ? ban.SoBan : request.SoBan;
            ban.SoNguoiToiDa = !request.SoNguoiToiDa.HasValue ? ban.SoNguoiToiDa : request.SoNguoiToiDa;
            ban.GiaTien = request.GiaTien.HasValue ? request.GiaTien : ban.GiaTien;
            ban.LoaiBanID = !request.LoaiBanID.HasValue ? ban.LoaiBanID : request.LoaiBanID;
            
            contextDB.Update(ban);
            await contextDB.SaveChangesAsync();
            return response.ResponseSuccess("Sửa bàn thành công", converters.EntityToDTOs(ban));

        }
        public async Task<ResponseObject<BanDTOs>> XoaBan(int id)
        {
            var ban = contextDB.Ban.SingleOrDefault(x => x.id == id);
            if (ban == null)
            {
                return response.ResponseError(StatusCodes.Status404NotFound, "Không có bàn cần Xoá", null);
            }
            var lstHD = contextDB.HoaDon.Where(x => x.BanID == ban.id).ToList();
            foreach (var x in lstHD)
            {
                var lstCTHD = contextDB.ChiTietHoaDon.Where(x => x.HoaDonID == x.id);
                contextDB.ChiTietHoaDon.RemoveRange(lstCTHD);
            }
            contextDB.HoaDon.RemoveRange(lstHD);
            contextDB.Ban.Remove(ban);
            return response.ResponseSuccess("Xoá bàn thành công", converters.EntityToDTOs(ban));
        }
        #endregion
    }
}
