using DatBanNhaHang.Entities.NhaHang;
using DatBanNhaHang.Payloads.Converters.NhaHang;
using DatBanNhaHang.Payloads.DTOs.NhaHang;
using DatBanNhaHang.Payloads.Requests.NhaHang.TrangThaiBan;
using DatBanNhaHang.Payloads.Responses;
using DatBanNhaHang.Services.Implements.DatBanNhaHang.Service.Implements;
using DatBanNhaHang.Services.IServices;

namespace DatBanNhaHang.Services.Implements
{
    public class TrangThaiBanServices : BaseService, ITrangThaiBan
    {
        private readonly TrangThaiBanConverters converters;
        private readonly ResponseObject<TrangThaiBanDTOs> response;

        public TrangThaiBanServices()
        {
            converters = new TrangThaiBanConverters();
            response = new ResponseObject<TrangThaiBanDTOs>();
        }
        public async Task<IQueryable<TrangThaiBanDTOs>> HienThiTrangThaiBan(int pageSize, int pageNumber)
        {
            return contextDB.TrangThaiBan.Select(x => converters.EntityToDTOs(x));
        }

        public async Task<ResponseObject<TrangThaiBanDTOs>> SuaTrangThaiBan(Request_SuaTrangThaiBan request)
        {
            var ttb = contextDB.TrangThaiBan.SingleOrDefault(x => x.id == request.TrangThaiBanID);
            if (ttb == null)
            {
                return response.ResponseError(StatusCodes.Status404NotFound, "Không có trạng thái bàn cần sửa ", null);
            }
            ttb.TenTrangThai = request.TenTrangThai==null?ttb.TenTrangThai:request.TenTrangThai;
            contextDB.TrangThaiBan.Update(ttb);
            await contextDB.SaveChangesAsync();
            return response.ResponseSuccess("Sửa trạng thái bàn thành công ", converters.EntityToDTOs(ttb));
        }

        public async Task<ResponseObject<TrangThaiBanDTOs>> ThemTrangThaiBan(Request_ThemTrangThaiBan request)
        {
            if(string.IsNullOrWhiteSpace(request.TenTrangThai))
            {
                return response.ResponseError(StatusCodes.Status404NotFound, "chưa điền đủ thông tin ", null);
            }
            TrangThaiBan ttb = new TrangThaiBan() { TenTrangThai = request.TenTrangThai };
             contextDB.TrangThaiBan.Add(ttb);
             await contextDB.SaveChangesAsync();
            return response.ResponseSuccess("Thêm trạng thái bàn thành công ", converters.EntityToDTOs(ttb));
        }

        public async Task<ResponseObject<TrangThaiBanDTOs>> XoaTrangThaiBan(Request_XoaTrangThaiBan request)
        {
          

            using (var trans = contextDB.Database.BeginTransaction())
            {
                try
                {
                    var ttb = contextDB.TrangThaiBan.SingleOrDefault(x => x.id == request.TrangThaiBanID);
                    if (ttb == null)
                    {
                        return response.ResponseError(StatusCodes.Status404NotFound, "Không có trạng thái bàn cần sửa ", null);
                    }
                    var ban = contextDB.Ban.Where(x => x.LoaiBanID == ttb.id).ToList();
                    foreach (var b in ban)
                    {
                        var lsthd = contextDB.HoaDon.Where(y => y.BanID == b.id).ToList();
                        foreach (var hd in lsthd)
                        {
                            var lstCTHD = contextDB.ChiTietHoaDon.Where(z => z.HoaDonID == hd.id);
                            contextDB.RemoveRange(lstCTHD);
                        }
                        contextDB.RemoveRange(lsthd);
                    }
                    contextDB.RemoveRange(ban);
                    contextDB.Remove(ttb);
                    await contextDB.SaveChangesAsync();
                    trans.Commit();
                    return response.ResponseSuccess("Xoá trạng thái bàn thành công ", converters.EntityToDTOs(ttb));
                }
                catch (Exception)
                {
                    trans.Rollback();
                    return response.ResponseError(StatusCodes.Status400BadRequest, "Lỗi", null);
                }
            }
        }
    }
}
