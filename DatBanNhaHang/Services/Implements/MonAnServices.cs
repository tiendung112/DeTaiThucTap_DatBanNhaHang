﻿using DatBanNhaHang.Entities.NhaHang;
using DatBanNhaHang.Handler.Image;
using DatBanNhaHang.Handler.Pagination;
using DatBanNhaHang.Payloads.Converters.NhaHang;
using DatBanNhaHang.Payloads.DTOs.NhaHang;
using DatBanNhaHang.Payloads.Requests.NhaHang.MonAn;
using DatBanNhaHang.Payloads.Responses;
using DatBanNhaHang.Services.Implements.DatBanNhaHang.Service.Implements;
using DatBanNhaHang.Services.IServices;

namespace DatBanNhaHang.Services.Implements
{
    public class MonAnServices : BaseService, IMonAn
    {
        private readonly ResponseObject<MonAnDTOs> response;
        private readonly MonAnConverters converters;
        public MonAnServices()
        {
            response = new ResponseObject<MonAnDTOs>();
            converters = new MonAnConverters();
        }
        #region hiển thị và tìm kiếm món ăn 
        public async Task<PageResult<MonAnDTOs>> HienThiMonAn(int id, int pageSize, int pageNumber)
        {
            var lstMonAn = id == 0 ? contextDB.MonAn.Where(y => y.status == 1).Select(x => converters.EntityToDTOs(x))
                : contextDB.MonAn.Where(y => y.id == id && y.status == 1).Select(x => converters.EntityToDTOs(x));
            var result = Pagintation.GetPagedData(lstMonAn, pageSize, pageNumber);
            return result;
        }
        public async Task<PageResult<MonAnDTOs>> TimKiemMonAn(string tenMonAn, int pageSize, int pageNumber)
        {
            var lstMonAn = contextDB.MonAn.AsEnumerable()
                .Where(x => ChuanHoaChuoi(x.TenMon).Contains(ChuanHoaChuoi(tenMonAn)))
                .Select(y => converters.EntityToDTOs(y)).AsQueryable();
            var res = Pagintation.GetPagedData(lstMonAn, pageSize, pageNumber);
            return res;
        }
        #endregion
        #region thêm , sửa , xoá món ăn 
        public async Task<ResponseObject<MonAnDTOs>> ThemMonAn(Request_ThemMonAn request)
        {
            if (string.IsNullOrWhiteSpace(request.TenMon) || request.LoaiMonAnID == 0 || string.IsNullOrWhiteSpace(request.MoTa) || request.GiaTien == null)
                return response.ResponseError(StatusCodes.Status404NotFound, "Chưa điền đủ nội dung ", null);
            if (!contextDB.LoaiMonAn.Any(x => x.id == request.LoaiMonAnID))
                return response.ResponseError(StatusCodes.Status404NotFound, "Không tồn tại loại món ăn này", null);

            else
            {
                int imageSize = 4344 * 5792;
                try
                {
                    MonAn ma = new MonAn();
                    ma.TenMon = request.TenMon;
                    ma.GiaTien = request.GiaTien;
                    ma.MoTa = request.MoTa;
                    ma.LoaiMonAnID = request.LoaiMonAnID;
                    ma.status = 2;
                    if (request.AnhMonAn1URL != null)
                    {
                        if (!HandleImage.IsImage(request.AnhMonAn1URL, imageSize))
                        {
                            return response.ResponseError(StatusCodes.Status400BadRequest, "Ảnh không hợp lệ", null);
                        }
                        else
                        {
                            var avatarFile = await HandleUploadImage.Upfile(request.AnhMonAn1URL, $"DatBanNhaHang/MonAn/{ma.LoaiMonAnID}");
                            ma.AnhMonAn1URL = avatarFile == "" ? "https://media.istockphoto.com/Id/1300845620/vector/user-icon-flat-isolated-on-white-background-user-symbol-vector-illustration.jpg?s=612x612&w=0&k=20&c=yBeyba0hUkh14_jgv1OKqIH0CCSWU_4ckRkAoy2p73o=" : avatarFile;
                        }
                    }

                    await contextDB.MonAn.AddAsync(ma);
                    await contextDB.SaveChangesAsync();
                    return response.ResponseSuccess("Thêm Món Ăn thành công", converters.EntityToDTOs(ma));
                }
                catch (Exception ex)
                {
                    return response.ResponseError(StatusCodes.Status500InternalServerError, ex.Message, null);
                }
            }
        }
        public async Task<ResponseObject<MonAnDTOs>> SuaMonAn(int id, Request_SuaMonAn request)
        {

            if (string.IsNullOrWhiteSpace(id.ToString()))
                return response.ResponseError(StatusCodes.Status404NotFound, "Chưa điền đủ nội dung ", null);
            if (!contextDB.LoaiMonAn.Any(x => x.id == request.LoaiMonAnID) && request.LoaiMonAnID != null)
                return response.ResponseError(StatusCodes.Status404NotFound, "Không tồn tại loại món ăn này", null);
            else
            {
                var ma = contextDB.MonAn.SingleOrDefault(x => x.id == id && x.status==1);
                if (ma == null)
                {
                    return response.ResponseError(StatusCodes.Status404NotFound, "Không tồn tại món ăn này ", null);
                }
                int imageSize = 4344 * 5792;
                try
                {

                    ma.TenMon = request.TenMon ?? ma.TenMon ;
                    ma.GiaTien = request.GiaTien ?? ma.GiaTien ;
                    ma.MoTa = request.MoTa ?? ma.MoTa;
                    ma.LoaiMonAnID = request.LoaiMonAnID == 0 ? ma.LoaiMonAnID : request.LoaiMonAnID;
                    if (request.AnhMonAn1URL != null)
                    {
                        if (!HandleImage.IsImage(request.AnhMonAn1URL, imageSize))
                        {
                            return response.ResponseError(StatusCodes.Status400BadRequest, "Ảnh không hợp lệ", null);
                        }
                        else
                        {
                            var avatarFile = await HandleUploadImage.Upfile(request.AnhMonAn1URL, $"DatBanNhaHang/MonAn/{ma.LoaiMonAnID}");
                            ma.AnhMonAn1URL = avatarFile;
                        }
                    }
                    contextDB.MonAn.Update(ma);
                    await contextDB.SaveChangesAsync();
                    return response.ResponseSuccess("Sửa Món Ăn thành công", converters.EntityToDTOs(ma));
                }
                catch (Exception ex)
                {
                    return response.ResponseError(StatusCodes.Status500InternalServerError, ex.Message, null);
                }
            }
        }
        public async Task<ResponseObject<MonAnDTOs>> XoaMonAn(int id)
        {
            var ma = contextDB.MonAn.SingleOrDefault(x => x.id == id && x.status==1);
            if (ma == null)
            {
                return response.ResponseError(StatusCodes.Status404NotFound, "Không tồn tại món ăn này ", null);
            }
            ma.status = 2;
            //var lstchitiethoadon = contextDB.ChiTietHoaDon.Where(x => x.MonAnID == ma.id).ToList();
            /*foreach (var item in lstchitiethoadon)
            {
                var hoadon = contextDB.HoaDon.SingleOrDefault(x => x.id == item.HoaDonID);
                var lstcthd = contextDB.ChiTietHoaDon.Where(x => x.HoaDonID == hoadon.id);
                contextDB.RemoveRange(lstcthd);
                contextDB.Remove(hoadon);
            }*/
            //contextDB.Remove(ma);
            contextDB.Update(ma);
            await contextDB.SaveChangesAsync();
            return response.ResponseSuccess("Xoá Món Ăn thành công", converters.EntityToDTOs(ma));
        }
        #endregion
    }
}
