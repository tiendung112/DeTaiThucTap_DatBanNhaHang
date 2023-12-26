using DatBanNhaHang.Entities.NhaHang;
using DatBanNhaHang.Handler.Email;
using DatBanNhaHang.Handler.Pagination;
using DatBanNhaHang.Payloads.Converters.NguoiDung;
using DatBanNhaHang.Payloads.DTOs.NguoiDung;
using DatBanNhaHang.Payloads.Requests.NguoiDung.User.LienHe;
using DatBanNhaHang.Payloads.Responses;
using DatBanNhaHang.Services.Implements.DatBanNhaHang.Service.Implements;
using DatBanNhaHang.Services.IServices;

namespace DatBanNhaHang.Services.Implements
{
    public class LienHeServices : BaseService, ILienHe
    {
        private readonly ResponseObject<LienHeDTOs> response;
        private readonly LienHeConverters converters;

        public LienHeServices()
        {
            response = new ResponseObject<LienHeDTOs>();
            converters = new LienHeConverters();
        }
        public async Task<PageResult<LienHeDTOs>> HienThiLienHe(int LienHeId, int pageSize, int pageNumber)
        {
            var LienHe = LienHeId == 0 ? contextDB.LienHe.Where(y=>y.status == 1).Select(x => converters.EntityToDTOs(x)) 
                : contextDB.LienHe.Where(y => y.id == LienHeId && y.status == 1).Select(x => converters.EntityToDTOs(x));
            var result = Pagintation.GetPagedData(LienHe, pageSize, pageNumber);
            return result;
        }

        public async Task<ResponseObject<LienHeDTOs>> ThemLienHe(Request_ThemLienHe request)
        {
            if (string.IsNullOrWhiteSpace(request.Hoten) || string.IsNullOrWhiteSpace(request.TieuDe)
                || string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.NoiDung))
            {
                return response.ResponseError(StatusCodes.Status404NotFound, "chưa điền đủ thông tin ", null);
            }
            if (Validate.IsValidEmail(request.Email) == false)
            {
                return response.ResponseError(StatusCodes.Status400BadRequest, "Định dạng Email không hợp lệ", null);
            }
            LienHe newfb = new LienHe()
            {
                Email = request.Email,
                Hoten = request.Hoten,
                NoiDung = request.NoiDung,
                DaTraLoi = false,
                ThoiGianGui = DateTime.Now,
                TieuDe = request.TieuDe,
                status = 1
            };
            await contextDB.AddAsync(newfb);
            await contextDB.SaveChangesAsync();
            return response.ResponseSuccess("Thêm liên hệ thành công", converters.EntityToDTOs(newfb));
        }

        public async Task<ResponseObject<LienHeDTOs>> XacNhanLienHe(int LienHeId)
        {
            var LienHe = contextDB.LienHe.SingleOrDefault(x => x.id == LienHeId && x.status==1);
            if (LienHe == null)
            {
                return response.ResponseError(StatusCodes.Status404NotFound, "Không tồn tại LienHe này", null);
            }
            LienHe.DaTraLoi = true;
            LienHe.ThoiGianTraLoi = DateTime.Now;
            contextDB.Update(LienHe);
            await contextDB.SaveChangesAsync();
            return response.ResponseSuccess("Xác nhận Liên Hệ thành công", converters.EntityToDTOs(LienHe));
        }

        public async Task<ResponseObject<LienHeDTOs>> XoaLienHe(int LienHeId)
        {
            var LienHe = contextDB.LienHe.SingleOrDefault(x => x.id == LienHeId && x.status==1);
            if (LienHe == null)
            {
                return response.ResponseError(StatusCodes.Status404NotFound, "Không tồn tại LienHe này", null);
            }
            LienHe.status = 2;
            contextDB.LienHe.Update(LienHe);
            await contextDB.SaveChangesAsync();
            return response.ResponseSuccess("Xoá Liên Hệ thành công", converters.EntityToDTOs(LienHe));
        }

        public async Task<List<LienHeDTOs>> XoaLienHeQuaLau()
        {
            DateTime quahan = DateTime.Now;
            List<LienHeDTOs> lh = new List<LienHeDTOs>();
            var LienHe = contextDB.LienHe.Where(x=> x.status==1).ToList();
            if (LienHe==null)
            {
                return null;
            }
            foreach (var item in LienHe)
            {
                if (item.ThoiGianGui.Value.AddDays(15) < quahan && item.ThoiGianTraLoi == null)
                {
                    item.status= 2;
                    contextDB.LienHe.Update(item);
                    lh.Add(converters.EntityToDTOs(item));
                }
            }
            await contextDB.SaveChangesAsync();
            return lh;
        }
    }
}
