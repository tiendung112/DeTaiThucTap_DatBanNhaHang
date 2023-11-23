using DatBanNhaHang.Entities.NhaHang;
using DatBanNhaHang.Handler.Image;
using DatBanNhaHang.Handler.Pagination;
using DatBanNhaHang.Payloads.Converters.NhaHang;
using DatBanNhaHang.Payloads.DTOs.NhaHang;
using DatBanNhaHang.Payloads.Requests.NhaHang.MonAn;
using DatBanNhaHang.Payloads.Responses;
using DatBanNhaHang.Services.Implements.DatBanNhaHang.Service.Implements;
using DatBanNhaHang.Services.IServices;
using Microsoft.VisualStudio.Services.WebApi;
using System.Drawing.Printing;

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

        public  async Task<IQueryable<MonAnDTOs>> HienThiMonAn(int pageSize, int pageNumber)
        {
            return contextDB.MonAn.Select(x => converters.EntityToDTOs(x));
        }

        public async Task<ResponseObject<MonAnDTOs>> SuaMonAn(Request_SuaMonAn request)
        {

            if (string.IsNullOrWhiteSpace(request.MonAnID.ToString()))
                return response.ResponseError(StatusCodes.Status404NotFound, "Chưa điền đủ nội dung ", null);
            if (!contextDB.LoaiMonAn.Any(x => x.id == request.LoaiMonAnID))
                return response.ResponseError(StatusCodes.Status404NotFound, "Không tồn tại loại món ăn này", null);
            else
            {
                var ma = contextDB.MonAn.SingleOrDefault(x => x.id == request.MonAnID);
                if (ma == null)
                {
                    return response.ResponseError(StatusCodes.Status404NotFound, "Không tồn tại món ăn này ", null);
                }
                int imageSize = 4344 * 5792;
                try
                {

                    ma.TenMon = request.TenMon ==null? ma.TenMon :request.TenMon;
                    ma.GiaTien = request.GiaTien ==null? ma.GiaTien :request.GiaTien;
                    ma.MoTa = request.MoTa==null? ma.MoTa : request.MoTa;
                    ma.LoaiMonAnID = request.LoaiMonAnID == null? ma.LoaiMonAnID :ma.LoaiMonAnID;
                    if (request.AnhMonAn1URL != null)
                    {
                        if (!HandleImage.IsImage(request.AnhMonAn1URL, imageSize))
                        {
                            return response.ResponseError(StatusCodes.Status400BadRequest, "Ảnh không hợp lệ", null);
                        }
                        else
                        {
                            var avatarFile = await HandleUploadImage.Upfile(request.AnhMonAn1URL, $"DatBanNhaHang/MonAn/{ma.LoaiMonAnID}");
                            ma.AnhMonAn1URL = avatarFile == "" ? ma.AnhMonAn1URL: avatarFile;
                        }
                    }
                    if (request.AnhMonAn2URL != null)
                    {
                        if (!HandleImage.IsImage(request.AnhMonAn2URL, imageSize))
                        {
                            return response.ResponseError(StatusCodes.Status400BadRequest, "Ảnh không hợp lệ", null);
                        }
                        else
                        {
                            var avatarFile = await HandleUploadImage.Upfile(request.AnhMonAn2URL, $"DatBanNhaHang/MonAn/{ma.LoaiMonAnID}");
                            ma.AnhMonAn2URL = avatarFile == "" ? ma.AnhMonAn2URL : avatarFile;
                        }
                    }
                    if (request.AnhMonAn3URL != null)
                    {
                        if (!HandleImage.IsImage(request.AnhMonAn3URL, imageSize))
                        {
                            return response.ResponseError(StatusCodes.Status400BadRequest, "Ảnh không hợp lệ", null);
                        }
                        else
                        {
                            var avatarFile = await HandleUploadImage.Upfile(request.AnhMonAn3URL, $"DatBanNhaHang/MonAn/{ma.LoaiMonAnID}");
                            ma.AnhMonAn3URL = avatarFile == "" ? ma.AnhMonAn3URL : avatarFile;
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

        public async Task<ResponseObject<MonAnDTOs>> ThemMonAn(Request_ThemMonAn request)
        {
            if (string.IsNullOrWhiteSpace(request.TenMon) || request.LoaiMonAnID==0 || string.IsNullOrWhiteSpace(request.MoTa)|| request.GiaTien == null)
                return response.ResponseError(StatusCodes.Status404NotFound, "Chưa điền đủ nội dung ", null) ;
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
                    if (request.AnhMonAn1URL != null )
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
                    if (request.AnhMonAn2URL != null)
                    {
                        if (!HandleImage.IsImage(request.AnhMonAn2URL, imageSize))
                        {
                            return response.ResponseError(StatusCodes.Status400BadRequest, "Ảnh không hợp lệ", null);
                        }
                        else
                        {
                            var avatarFile = await HandleUploadImage.Upfile(request.AnhMonAn2URL, $"DatBanNhaHang/MonAn/{ma.LoaiMonAnID}");
                            ma.AnhMonAn2URL = avatarFile == "" ? "https://media.istockphoto.com/Id/1300845620/vector/user-icon-flat-isolated-on-white-background-user-symbol-vector-illustration.jpg?s=612x612&w=0&k=20&c=yBeyba0hUkh14_jgv1OKqIH0CCSWU_4ckRkAoy2p73o=" : avatarFile;
                        }
                    }
                    if (request.AnhMonAn3URL != null)
                    {
                        if (!HandleImage.IsImage(request.AnhMonAn3URL, imageSize))
                        {
                            return response.ResponseError(StatusCodes.Status400BadRequest, "Ảnh không hợp lệ", null);
                        }
                        else
                        {
                            var avatarFile = await HandleUploadImage.Upfile(request.AnhMonAn3URL, $"DatBanNhaHang/MonAn/{ma.LoaiMonAnID}");
                            ma.AnhMonAn3URL = avatarFile == "" ? "https://media.istockphoto.com/Id/1300845620/vector/user-icon-flat-isolated-on-white-background-user-symbol-vector-illustration.jpg?s=612x612&w=0&k=20&c=yBeyba0hUkh14_jgv1OKqIH0CCSWU_4ckRkAoy2p73o=" : avatarFile;
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

        public async Task<IQueryable<MonAnDTOs>> TimKiemMonAn(Request_TimKiemMonAn request,int pageSize, int pageNumber)
        {
            return contextDB.MonAn.Where(x => ChuanHoaChuoi(x.TenMon) == ChuanHoaChuoi(request.TenMon))
                .Select(y => converters.EntityToDTOs(y));
        }

        public async Task<ResponseObject<MonAnDTOs>> XoaMonAn(Request_XoaMonAn request)
        {
            var ma = contextDB.MonAn.SingleOrDefault(x => x.id == request.MonAnID);
            if(ma == null)
            {
                return response.ResponseError(StatusCodes.Status404NotFound, "Không tồn tại món ăn này ", null);
            }
            contextDB.Remove(ma);
            await contextDB.SaveChangesAsync();
            return response.ResponseSuccess("Xoá Món Ăn thành công", converters.EntityToDTOs(ma));
        }
    }
}
