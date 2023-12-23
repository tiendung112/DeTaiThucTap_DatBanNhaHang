using DatBanNhaHang.Entities.NhaHang;
using DatBanNhaHang.Handler.Image;
using DatBanNhaHang.Handler.Pagination;
using DatBanNhaHang.Payloads.Converters.NguoiDung;
using DatBanNhaHang.Payloads.DTOs.NguoiDung;
using DatBanNhaHang.Payloads.Requests.NguoiDung.Admin.Blog;
using DatBanNhaHang.Payloads.Responses;
using DatBanNhaHang.Services.Implements.DatBanNhaHang.Service.Implements;
using DatBanNhaHang.Services.IServices;

namespace DatBanNhaHang.Services.Implements
{
    public class BaiVietServices : BaseService, IBaiViet
    {
        private readonly ResponseObject<BaiVietDTOs> response;
        private readonly BaiVietConverters converters;
        public BaiVietServices()
        {
            response = new ResponseObject<BaiVietDTOs>();
            converters = new BaiVietConverters();
        }
        public async Task<PageResult<BaiVietDTOs>> HienThiBaiViet(int blogid, int pageSize, int pageNumber)
        {
            var blog = blogid == 0 ? contextDB.BaiViet.Select(x => converters.EntityToDTOs(x)) : contextDB.BaiViet.Where(y => y.id == blogid).Select(x => converters.EntityToDTOs(x));
            var result = Pagintation.GetPagedData(blog, pageSize, pageNumber);
            return result;
        }

        public async Task<ResponseObject<BaiVietDTOs>> SuaBaiViet(int blogid, int adminid, Request_SuaBaiViet request)
        {
            var baiviet = contextDB.BaiViet.SingleOrDefault(x => x.id == blogid);
            if (baiviet == null)
            {
                return response.ResponseError(StatusCodes.Status404NotFound, "không tồn tại bài viết này", null);
            }
            baiviet.TieuDe = request.TieuDe == null ? baiviet.TieuDe : baiviet.TieuDe;
            baiviet.NoiDung = request.NoiDung == null ? baiviet.NoiDung : baiviet.NoiDung;
            baiviet.NgayDang = DateTime.Now;
            baiviet.AdminId = adminid;

            int imageSize = 4344 * 5792;
            if (request.AnhBlogURl != null)
            {
                if (!HandleImage.IsImage(request.AnhBlogURl, imageSize))
                {
                    return response.ResponseError(StatusCodes.Status400BadRequest, "Ảnh không hợp lệ", null);
                }
                else
                {
                    var avatarFile = await HandleUploadImage.Upfile(request.AnhBlogURl, $"DatBanNhaHang/BaiViet/{baiviet.id}");
                    baiviet.AnhBlogURl = avatarFile == "" ? baiviet.AnhBlogURl : avatarFile;
                }
            }
            contextDB.Update(baiviet);
            await contextDB.SaveChangesAsync();
            return response.ResponseSuccess("sửa bài viết thành công", converters.EntityToDTOs(baiviet));
        }

        public async Task<ResponseObject<BaiVietDTOs>> ThemBaiViet(int admid, Request_ThemBaiViet request)
        {
            if (string.IsNullOrWhiteSpace(request.TieuDe) || string.IsNullOrWhiteSpace(request.MoTa) || string.IsNullOrWhiteSpace(request.NoiDung))
            {
                return response.ResponseError(StatusCodes.Status404NotFound, "chưa nhập đầy đủ thông tin", null);
            }
            BaiViet newbaiviet = new BaiViet()
            {
                AdminId = admid,
                MoTa = request.MoTa,
                NgayDang = DateTime.Now,
                NoiDung = request.NoiDung,
                TieuDe = request.TieuDe,
            };
            await contextDB.AddAsync(newbaiviet);
            await contextDB.SaveChangesAsync();

            int imageSize = 4344 * 5792;
            if (request.AnhBlogURl != null)
            {
                if (!HandleImage.IsImage(request.AnhBlogURl, imageSize))
                {
                    return response.ResponseError(StatusCodes.Status400BadRequest, "Ảnh không hợp lệ", null);
                }
                else
                {
                    var avatarFile = await HandleUploadImage.Upfile(request.AnhBlogURl, $"DatBanNhaHang/BaiViet/{newbaiviet.id}");
                    newbaiviet.AnhBlogURl = avatarFile == "" ? "https://www.bootdey.com/image/800x350/87CEFA/000000" : avatarFile;
                }
            }
            contextDB.Update(newbaiviet);
            await contextDB.SaveChangesAsync();
            return response.ResponseSuccess("thêm bài viết thành công", converters.EntityToDTOs(newbaiviet));
        }

        public async Task<ResponseObject<BaiVietDTOs>> XoaBaiViet(int blogid)
        {
            var baiviet = contextDB.BaiViet.SingleOrDefault(x => x.id == blogid);
            if (baiviet == null)
            {
                return response.ResponseError(StatusCodes.Status404NotFound, "không tồn tại bài viết này", null);
            }
            contextDB.Remove(baiviet);
            await contextDB.SaveChangesAsync();
            return response.ResponseSuccess("Xoá bài viết thành công", converters.EntityToDTOs(baiviet));
        }
    }
}
