using DatBanNhaHang.Entities.NhaHang;
using DatBanNhaHang.Handler.Image;
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
            var ban = id == 0 ? contextDB.Ban.Where(y=>y.status==1).Select(x => converters.EntityToDTOs(x))
                : contextDB.Ban.Where(y => y.id == id&& y.status==1).Select(x => converters.EntityToDTOs(x));
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
        #endregion

        #region thêm sửa xoá bàn
        public async Task<ResponseObject<BanDTOs>> ThemBan(Request_ThemBan request)
        {
            if (string.IsNullOrWhiteSpace(request.ViTri) || !request.GiaTien.HasValue || !request.SoBan.HasValue
                || !request.SoNguoiToiDa.HasValue || !request.GiaTien.HasValue || !request.LoaiBanID.HasValue)
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
                Mota = request.Mota,
                //TinhTrangHienTai = request.TinhTrangHienTai
                status = 1,
            };
            contextDB.Ban.Add(newBan);
            await contextDB.SaveChangesAsync();
            int imageSize = 4344 * 5792;
            if (request.HinhAnhBanURL != null)
            {
                if (!HandleImage.IsImage(request.HinhAnhBanURL, imageSize))
                {
                    return response.ResponseError(StatusCodes.Status400BadRequest, "Ảnh không hợp lệ", null);
                }
                else
                {
                    var avatarFile = await HandleUploadImage.Upfile(request.HinhAnhBanURL, $"DatBanNhaHang/LoaiBan/{newBan.LoaiBanID}");
                    newBan.HinhAnhBanURL = avatarFile == "" ? "null" : avatarFile;
                }
            }
            contextDB.Ban.Update(newBan);
            await contextDB.SaveChangesAsync();
            return response.ResponseSuccess("thêm bàn thành công", converters.EntityToDTOs(newBan));
        }
        public async Task<ResponseObject<BanDTOs>> SuaBan(int id, Request_SuaBan request)
        {
            if (!contextDB.Ban.Any(x => x.LoaiBanID == request.LoaiBanID) && request.LoaiBanID != null)
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
            //ban.TinhTrangHienTai = request.TinhTrangHienTai == null ? ban.TinhTrangHienTai : request.TinhTrangHienTai;
            ban.Mota = request.Mota == null ? ban.Mota : request.Mota;
            int imageSize = 4344 * 5792;
            if (request.HinhAnhBanURL != null)
            {
                if (!HandleImage.IsImage(request.HinhAnhBanURL, imageSize))
                {
                    return response.ResponseError(StatusCodes.Status400BadRequest, "Ảnh không hợp lệ", null);
                }
                else
                {
                    var avatarFile = await HandleUploadImage.Upfile(request.HinhAnhBanURL, $"DatBanNhaHang/LoaiBan/{ban.LoaiBanID}");
                    ban.HinhAnhBanURL = avatarFile == "" ? ban.HinhAnhBanURL : avatarFile;
                }
            }
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
            ban.status = 2;
            var lstHD = contextDB.HoaDon.Where(x => x.BanID == ban.id).ToList();
            if(lstHD.Count > 0)
            {
                foreach (var x in lstHD)
                {
                    x.status = 2;
                    /*var lstCTHD = contextDB.ChiTietHoaDon.Where(x => x.HoaDonID == x.id);
                    contextDB.ChiTietHoaDon.RemoveRange(lstCTHD);*/
                }
                contextDB.HoaDon.UpdateRange(lstHD);

            }
            contextDB.Ban.Update(ban);
            await contextDB.SaveChangesAsync();
            return response.ResponseSuccess("Xoá bàn thành công", converters.EntityToDTOs(ban));
        }
        #endregion
    }
}
