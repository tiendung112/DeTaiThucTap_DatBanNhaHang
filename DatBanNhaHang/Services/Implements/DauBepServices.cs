using DatBanNhaHang.Entities.NhaHang;
using DatBanNhaHang.Handler.Pagination;
using DatBanNhaHang.Payloads.Converters.NhaHang;
using DatBanNhaHang.Payloads.DTOs.NhaHang;
using DatBanNhaHang.Payloads.Requests.NhaHang.DauBep;
using DatBanNhaHang.Payloads.Responses;
using DatBanNhaHang.Services.Implements.DatBanNhaHang.Service.Implements;
using DatBanNhaHang.Services.IServices;
using DatBanNhaHang.Handler.Image;
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
        #region hiển thị  và tìm kiếm danh sách đầu bếp
        public async Task<PageResult<DauBepDTOs>> GetDSDauBep(int id, int pageSize, int pageNumber)
        {
            var lstDauBep = id == 0 ? context.DauBep.Select(x => converters.EntityToDTOs(x))
                : context.DauBep.Where(y => y.id == id).Select(z => converters.EntityToDTOs(z));
            var result = Pagintation.GetPagedData(lstDauBep, pageSize, pageNumber);
            return result;
        }

        #endregion
        #region thêm sửa xoá đầu bếp

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
                    // db.AnhDauBepURl = request.AnhDauBepURl;
                    if (request.AnhDauBepURl != null)
                    {
                        if (!HandleImage.IsImage(request.AnhDauBepURl, imageSize))
                        {
                            return response.ResponseError(StatusCodes.Status400BadRequest, "Ảnh không hợp lệ", null);
                        }
                        else
                        {
                            var avatarFile = await HandleUploadImage.Upfile(request.AnhDauBepURl, "DatBanNhaHang/DauBep");
                            db.AnhDauBepURl = avatarFile == "" ? "null" : avatarFile;
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
        public async Task<ResponseObject<DauBepDTOs>> SuaDauBep(int id, Request_SuaDauBep request)
        {
            var daubep = context.DauBep.FirstOrDefault(x => x.id == id);
            if (daubep == null)
                return response.ResponseError(403, "Không tồn tại đầu bếp này", null);
            else
            {
                int imageSize = 2 * 1024 * 768;
                try
                {
                    daubep.HoTen = request.HoTen == null ? daubep.HoTen : request.HoTen;
                    daubep.ngaySinh = request.ngaySinh == null ? daubep.ngaySinh : request.ngaySinh;
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
                            daubep.AnhDauBepURl = avatarFile == "" ? daubep.AnhDauBepURl : avatarFile;
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
        public async Task<ResponseObject<DauBepDTOs>> XoaDauBep(int id)
        {
            var daubep = context.DauBep.FirstOrDefault(x => x.id ==id);
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
        #endregion
    }
}
