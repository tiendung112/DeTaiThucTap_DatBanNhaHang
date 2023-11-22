using DatBanNhaHang.Entities.NhaHang;
using DatBanNhaHang.Handler.Image;
using DatBanNhaHang.Pagination;
using DatBanNhaHang.Payloads.Converters.NguoiDung;
using DatBanNhaHang.Payloads.Converters.NhaHang;
using DatBanNhaHang.Payloads.DTOs.NhaHang;
using DatBanNhaHang.Payloads.Requests.NhaHang.DauBep;
using DatBanNhaHang.Payloads.Responses;
using DatBanNhaHang.Services.Implements.DatBanNhaHang.Service.Implements;
using DatBanNhaHang.Services.IServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Services.WebApi;
using System;

namespace DatBanNhaHang.Services.Implements
{
    public class DauBepServices : BaseService, IDauBep
    {
        private readonly Context.AppDbContext context;
        private readonly ResponseObject<DauBepDTOs> response;
        private readonly DauBepConverters converters;

        public DauBepServices()
        {
            context = new Context.AppDbContext();
            response = new ResponseObject<DauBepDTOs>();
            converters = new DauBepConverters();
        }
        public async Task<IQueryable<DauBepDTOs>> GetDSDauBep(Pagintation pagintation)
        { 
            var lstDauBep = context.DauBep.Select(x => converters.EntityToDTOs(x));
            return lstDauBep;
        }

        public async Task<ResponseObject<DauBepDTOs>> SuaDauBep(Request_SuaDauBep request)
        {
            var daubep = context.DauBep.FirstOrDefault(x => x.id == request.ID);
            if (daubep == null)
                return response.ResponseError(403, "Không tồn tại đầu bếp này", null);
            else
            {
                int imageSize = 2 * 1024 * 768;
                try
                { 
                    daubep.HoTen = request.HoTen==null? daubep.HoTen : request.HoTen;
                    daubep.ngaySinh = request.ngaySinh ==null ? daubep.ngaySinh : request.ngaySinh;
                    daubep.SDT = request.SDT == null ? daubep.SDT : request.SDT;
                    daubep.MoTa = request.MoTa == null ? daubep.MoTa : request.MoTa;
                    if (request.AnhDauBepURl != null)
                    {
                        if (!HandleImage.IsImage(request.AnhDauBepURl, imageSize))
                        {
                            return response.ResponseError(StatusCodes.Status400BadRequest, "Ảnh không hợp lệ", null);
                        }
                        else
                        {
                            var avatarFile = await HandleUploadImage.Upfile(request.AnhDauBepURl, "DatBanNhaHang/DauBep");
                            daubep.AnhDauBepURl = avatarFile == "" ? daubep.AnhDauBepURl: avatarFile;
                        }
                    }
                      contextDB.DauBep.Update(daubep);
                     await contextDB.SaveChangesAsync();
                    return response.ResponseSuccess("Sửa Đầu Bếp thành công", converters.EntityToDTOs(daubep));


                }
                catch (Exception ex)
                {
                    return response.ResponseError(StatusCodes.Status500InternalServerError, ex.Message, null);
                }

            }
        }

        public async Task<ResponseObject<DauBepDTOs>> ThemDauBep(Request_ThemDauBep request)
        {
            if (string.IsNullOrWhiteSpace(request.HoTen)
                || string.IsNullOrWhiteSpace(request.MoTa))
            {
                return response.ResponseError(StatusCodes.Status404NotFound, "Bạn cần truyền vào đầy đủ thông tin", null);

            }
            else
            {
                int imageSize = 2 * 1024 * 768;
                try
                {
                    DauBep db = new DauBep();
                    db.HoTen = request.HoTen;
                    db.ngaySinh = request.ngaySinh;
                    db.SDT = request.SDT;
                    db.MoTa = request.MoTa;
                    if (request.AnhDauBepURl != null)
                    {
                        if (!HandleImage.IsImage(request.AnhDauBepURl, imageSize))
                        {
                            return response.ResponseError(StatusCodes.Status400BadRequest, "Ảnh không hợp lệ", null);
                        }
                        else
                        {
                            var avatarFile = await HandleUploadImage.Upfile(request.AnhDauBepURl, "DatBanNhaHang/DauBep");
                            db.AnhDauBepURl = avatarFile == "" ? "https://cdn-icons-png.flaticon.com/512/562/562678.png" : avatarFile;
                        }
                    }
                    await contextDB.DauBep.AddAsync(db);
                    await contextDB.SaveChangesAsync();
                    return response.ResponseSuccess("Thêm Đầu Bếp  thành công", converters.EntityToDTOs(db));


                }
                catch (Exception ex)
                {
                    return response.ResponseError(StatusCodes.Status500InternalServerError, ex.Message, null);
                }
            }
        }

        public async Task<ResponseObject<DauBepDTOs>> XoaDauBep(Request_XoaDauBep request)
        {
            var daubep = context.DauBep.FirstOrDefault(x => x.id == request.ID);
            if (daubep != null)
            {
                context.DauBep.Remove(daubep);
                await context.SaveChangesAsync();
                return response.ResponseSuccess("Xoá Đầu Bếp Thành công", converters.EntityToDTOs(daubep));
            }
            else
            {
                return response.ResponseError(403, "không tồn tại đầu bếp này", null);
            }
        }
    }
}
