using DatBanNhaHang.Entities.NhaHang;
using DatBanNhaHang.Handler.Image;
using DatBanNhaHang.Handler.Pagination;
using DatBanNhaHang.Payloads.Converters.NguoiDung;
using DatBanNhaHang.Payloads.DTOs.NguoiDung;
using DatBanNhaHang.Payloads.Requests.NguoiDung.Admin.NhanXet;
using DatBanNhaHang.Payloads.Responses;
using DatBanNhaHang.Services.Implements.DatBanNhaHang.Service.Implements;
using DatBanNhaHang.Services.IServices;

namespace DatBanNhaHang.Services.Implements
{
    public class NhanXetServices : BaseService, INhanXet
    {
        private readonly ResponseObject<NhanXetDTOs> response;
        private readonly NhanXetConverters converters;
        public NhanXetServices()
        {
            response = new ResponseObject<NhanXetDTOs>();
            converters = new NhanXetConverters();

        }
        public async Task<PageResult<NhanXetDTOs>> HienThiNhanXet(int nhanxetid, int pageSize, int pageNumber)
        {
            var lstnx = nhanxetid != 0 ? 
                contextDB.NhanXet.Where(y => y.id == nhanxetid&& y.status == 1)
                    .Select(x => converters.EntityToDTOs(x)) 
                : contextDB.NhanXet.Where(y=> y.status == 1)
                    .Select(x => converters.EntityToDTOs(x));
            var result = Pagintation.GetPagedData(lstnx, pageSize, pageNumber);
            return result;
        }

        public async Task<ResponseObject<NhanXetDTOs>> SuaNhanXet(int nhanxetid, Request_SuaNhanXet request)
        {
            var nhanxet = contextDB.NhanXet.SingleOrDefault(x => x.id == nhanxetid && x.status==1);
            if (nhanxet == null)
            {
                return response.ResponseError(StatusCodes.Status404NotFound, "không tồn tại nhận xét này", null);
            }
            int imageSize = 4344 * 5792;
            if (request.AnhURL != null)
            {
                if (!HandleImage.IsImage(request.AnhURL, imageSize))
                {
                    return response.ResponseError(StatusCodes.Status400BadRequest, "Ảnh không hợp lệ", null);
                }
                else
                {
                    var avatarFile = await HandleUploadImage.Upfile(request.AnhURL, $"DatBanNhaHang/MonAn/{nhanxet.id}");
                    nhanxet.AnhURL = avatarFile == "" ? nhanxet.AnhURL : avatarFile;
                }
            }
            nhanxet.ChuThich = request.ChuThich ?? nhanxet.ChuThich;
            nhanxet.HoTen = request.HoTen ?? nhanxet.HoTen ;
            nhanxet.NoiDung = request.NoiDung?? nhanxet.NoiDung;
            contextDB.Update(nhanxet);
            await contextDB.SaveChangesAsync();
            return response.ResponseSuccess("sửa nhận xét thành công", converters.EntityToDTOs(nhanxet));
        }

        public async Task<ResponseObject<NhanXetDTOs>> ThemNhanXet(Request_ThemNhanXet request)
        {
            if (string.IsNullOrWhiteSpace(request.HoTen) || string.IsNullOrWhiteSpace(request.ChuThich) || string.IsNullOrWhiteSpace(request.NoiDung))
            {
                return response.ResponseError(StatusCodes.Status404NotFound, "Chưa điền đủ thông tin", null);
            }
            NhanXet newNhanXet = new NhanXet()
            {
                ChuThich = request.ChuThich,
                HoTen = request.HoTen,
                NoiDung = request.NoiDung,
                status = 1
            };
            await contextDB.AddAsync(newNhanXet);
            await contextDB.SaveChangesAsync();
            int imageSize = 4344 * 5792;

            if (request.AnhURL != null)
            {
                if (!HandleImage.IsImage(request.AnhURL, imageSize))
                {
                    return response.ResponseError(StatusCodes.Status400BadRequest, "Ảnh không hợp lệ", null);
                }
                else
                {
                    var avatarFile = await HandleUploadImage.Upfile(request.AnhURL, $"DatBanNhaHang/MonAn/{newNhanXet.id}");
                    newNhanXet.AnhURL = avatarFile == "" ? "https://media.istockphoto.com/Id/1300845620/vector/user-icon-flat-isolated-on-white-background-user-symbol-vector-illustration.jpg?s=612x612&w=0&k=20&c=yBeyba0hUkh14_jgv1OKqIH0CCSWU_4ckRkAoy2p73o=" : avatarFile;
                }
            }
            contextDB.Update(newNhanXet);
            await contextDB.SaveChangesAsync();
            return response.ResponseSuccess("Thêm nhận xét thành công", converters.EntityToDTOs(newNhanXet));
        }

        public async Task<ResponseObject<NhanXetDTOs>> XoaNhanXet(int nhanxetid)
        {
            var nhanxet = contextDB.NhanXet.SingleOrDefault(x => x.id == nhanxetid && x.status==1);
            if (nhanxet == null)
            {
                return response.ResponseError(StatusCodes.Status404NotFound, "không tồn tại nhận xét này", null);
            }
            nhanxet.status = 2;
            contextDB.Update(nhanxet);
            await contextDB.SaveChangesAsync();
            return response.ResponseSuccess("Xoá nhận xét thành công", converters.EntityToDTOs(nhanxet));
        }
    }
}
