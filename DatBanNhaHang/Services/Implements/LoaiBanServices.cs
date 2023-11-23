using DatBanNhaHang.Entities.NhaHang;
using DatBanNhaHang.Payloads.Converters.NhaHang;
using DatBanNhaHang.Payloads.DTOs.NhaHang;
using DatBanNhaHang.Payloads.Requests.NhaHang.LoaiBan;
using DatBanNhaHang.Payloads.Responses;
using DatBanNhaHang.Services.Implements.DatBanNhaHang.Service.Implements;
using DatBanNhaHang.Services.IServices;

namespace DatBanNhaHang.Services.Implements
{
    public class LoaiBanServices : BaseService, ILoaiBan
    {
        private readonly LoaiBanConverters converters;
        private readonly ResponseObject<LoaiBanDTOs> response;
        public LoaiBanServices()
        {
            converters = new LoaiBanConverters();
            response = new ResponseObject<LoaiBanDTOs>();
        }
        public async Task<IQueryable<LoaiBanDTOs>> HienThiLoaiBan(int pageSize, int pageNumber)
        {
            return contextDB.LoaiBan.Select(x => converters.EntityToDTOs(x));
        }

        public async Task<ResponseObject<LoaiBanDTOs>> SuaLoaiBan(Request_SuaLoaiBan request)
        {
            var lb = contextDB.LoaiBan.SingleOrDefault(x => x.id == request.LoaiBanID);
            if (lb == null)
            {
                return response.ResponseError(StatusCodes.Status404NotFound, "Không có loại bàn cần sửa ", null);
            }
            lb.TenLoaiBan = request.TenLoaiBan==null?lb.TenLoaiBan:request.TenLoaiBan;
            contextDB.Update(lb);
            await contextDB.SaveChangesAsync();
            return response.ResponseSuccess("Sửa loại bàn thành công ", converters.EntityToDTOs(lb));
        }

        public async Task<ResponseObject<LoaiBanDTOs>> ThemLoaiBan(Request_ThemLoaiBan request)
        {
            if (string.IsNullOrWhiteSpace(request.TenLoaiBan))
            {
                return response.ResponseError(StatusCodes.Status404NotFound, "chưa điền đủ thông tin ", null);
            }
            LoaiBan loaiBan = new LoaiBan()
            {
                TenLoaiBan = request.TenLoaiBan,
            };
            contextDB.Add(loaiBan);
            await contextDB.SaveChangesAsync();
            return response.ResponseSuccess("Thêm loại bàn thành công ", converters.EntityToDTOs(loaiBan));
        }

        public async Task<ResponseObject<LoaiBanDTOs>> XoaLoaiBan(Request_XoaLoaiBan request)
        {
            using (var trans = contextDB.Database.BeginTransaction())
            {
                try
                {
                    var lb = contextDB.LoaiBan.SingleOrDefault(x => x.id == request.LoaiBanID);
                    if (lb == null)
                    {
                        return response.ResponseError(StatusCodes.Status404NotFound, "Không có loại bàn cần xoá ", null);
                    }
                    var ban = contextDB.Ban.Where(x => x.LoaiBanID == lb.id).ToList();
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
                    contextDB.Remove(lb);
                    await contextDB.SaveChangesAsync();
                    trans.Commit();
                    return response.ResponseSuccess("Xoá loại bàn thành công ", converters.EntityToDTOs(lb));
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
