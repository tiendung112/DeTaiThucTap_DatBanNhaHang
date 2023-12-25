using DatBanNhaHang.Entities.NhaHang;
using DatBanNhaHang.Handler.Pagination;
using DatBanNhaHang.Payloads.Converters.NhaHang;
using DatBanNhaHang.Payloads.DTOs.NhaHang;
using DatBanNhaHang.Payloads.Requests.NhaHang.TrangThaiHoaDon;
using DatBanNhaHang.Payloads.Responses;
using DatBanNhaHang.Services.Implements.DatBanNhaHang.Service.Implements;
using DatBanNhaHang.Services.IServices;

namespace DatBanNhaHang.Services.Implements
{
    public class TrangThaiHoaDonServices : BaseService, ITrangThaiHoaDon
    {
        private readonly TrangThaiHoaDonConverters converters;
        private readonly ResponseObject<TrangThaiHoaDonDTOs> response;
        public TrangThaiHoaDonServices()
        {
            converters = new TrangThaiHoaDonConverters();
            response = new ResponseObject<TrangThaiHoaDonDTOs>();
        }
        public async Task<PageResult<TrangThaiHoaDonDTOs>> HienThiTrangThaiHoaDon(int id, int pageSize, int pageNumber)
        {
            var lstMonAn = id == 0 ? 
                contextDB.TrangThaiHoaDon.Where(y=>y.status==1).Select(x => converters.EntityToDTOs(x)) 
                : contextDB.TrangThaiHoaDon.Where(y => y.id == id&& y.status==1).Select(x => converters.EntityToDTOs(x));
            var result = Pagintation.GetPagedData(lstMonAn, pageSize, pageNumber);
            return result;
        }

        public async Task<ResponseObject<TrangThaiHoaDonDTOs>> SuaTrangThaiHoaDon(int id, Request_SuaTrangThaiHoaDon request)
        {
            var tthd = contextDB.TrangThaiHoaDon.SingleOrDefault(x => x.id == id);
            if (tthd == null)
            {
                return response.ResponseError(StatusCodes.Status404NotFound, "Không tồn tại trạng thái này ", null);
            }
            tthd.TenTrangThai = request.TenTrangThai == null ? tthd.TenTrangThai : request.TenTrangThai;
            contextDB.Update(tthd);
            await contextDB.SaveChangesAsync();
            return response.ResponseSuccess("Sửa trạng thái thành công ", converters.EntityToDTOs(tthd));
        }

        public async Task<ResponseObject<TrangThaiHoaDonDTOs>> ThemTrangThaiHoaDon(Request_ThemTrangThaiHoaDon request)
        {
            if (string.IsNullOrWhiteSpace(request.TenTrangThai))
                return response.ResponseError(StatusCodes.Status404NotFound, "chưa điền  đầy đủ thông tin ", null);
            TrangThaiHoaDon tthd = new TrangThaiHoaDon()
            {
                TenTrangThai = request.TenTrangThai,
                status = 1,
            };
            contextDB.TrangThaiHoaDon.Add(tthd);
            await contextDB.SaveChangesAsync();
            return response.ResponseSuccess("Thêm trạng thái thành công ", converters.EntityToDTOs(tthd));
        }

        public async Task<ResponseObject<TrangThaiHoaDonDTOs>> XoaTrangThaiHoaDon(int id)
        {
            var tthd = contextDB.TrangThaiHoaDon.SingleOrDefault(x => x.id == id);
            if (tthd == null)
            {
                return response.ResponseError(StatusCodes.Status404NotFound, "Không tồn tại trạng thái này ", null);
            }

            tthd.status = 2;
            var lsthd = contextDB.HoaDon.Where(x => x.id == tthd.id).ToList();
            foreach (var hd in lsthd)
            {
                hd.status = 2;
                /*var lstCT = contextDB.ChiTietHoaDon.Where(x => x.HoaDonID == hd.id);
                contextDB.RemoveRange(lstCT);*/
            }
            contextDB.Update(lsthd);
            contextDB.Update(tthd);
            await contextDB.SaveChangesAsync();
            return response.ResponseSuccess("Xoá trạng thái thành công ", converters.EntityToDTOs(tthd));
        }
    }
}
