﻿using DatBanNhaHang.Entities.NhaHang;
using DatBanNhaHang.Handler.Pagination;
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

        #region hiển thị và tìm kiếm loại bàn
        public async Task<PageResult<SingleLoaiBanDTOs>> HienThiLoaiBan(int id, int pageSize, int pageNumber)
        {
            var lst = id == 0 ?
                contextDB.LoaiBan.Where(y=>y.status==1).Select(x => converters.EntitySingleLoaiBanToDTOs(x))
                : contextDB.LoaiBan.Where(y => y.id == id&& y.status==1).Select(x => converters.EntitySingleLoaiBanToDTOs(x));
            var result = Pagintation.GetPagedData(lst, pageSize, pageNumber);
            return result;
        }

        public async Task<PageResult<LoaiBanDTOs>> HienThiLoaiBanKemBan(int id, int pageSize, int pageNumber)
        {
            var lst = id == 0 ? contextDB.LoaiBan.Where(y=>y.status==1).Select(x => converters.EntityToDTOs(x)) 
                : contextDB.LoaiBan.Where(y => y.id == id&& y.status==1).Select(x => converters.EntityToDTOs(x));
            var result = Pagintation.GetPagedData(lst, pageSize, pageNumber);
            return result;
        }
        #endregion

        #region  thêm, sửa , xoá loại bàn
        public async Task<ResponseObject<LoaiBanDTOs>> ThemLoaiBan(Request_ThemLoaiBan request)
        {
            if (string.IsNullOrWhiteSpace(request.TenLoaiBan))
            {
                return response.ResponseError(StatusCodes.Status404NotFound, "chưa điền đủ thông tin ", null);
            }
            LoaiBan loaiBan = new LoaiBan()
            {
                TenLoaiBan = request.TenLoaiBan,
                status = 1
            };
            contextDB.Add(loaiBan);
            await contextDB.SaveChangesAsync();
            return response.ResponseSuccess("Thêm loại bàn thành công ", converters.EntityToDTOs(loaiBan));
        }
        public async Task<ResponseObject<LoaiBanDTOs>> SuaLoaiBan(int id, Request_SuaLoaiBan request)
        {
            var lb = contextDB.LoaiBan.SingleOrDefault(x => x.id == id && x.status==1);
            if (lb == null)
            {
                return response.ResponseError(StatusCodes.Status404NotFound, "Không có loại bàn cần sửa ", null);
            }
            lb.TenLoaiBan = request.TenLoaiBan  ?? lb.TenLoaiBan;
            contextDB.Update(lb);
            await contextDB.SaveChangesAsync();
            return response.ResponseSuccess("Sửa loại bàn thành công ", converters.EntityToDTOs(lb));
        }

        public async Task<ResponseObject<LoaiBanDTOs>> XoaLoaiBan(int id)
        {
            using (var trans = contextDB.Database.BeginTransaction())
            {
                try
                {
                    var lb = contextDB.LoaiBan.SingleOrDefault(x => x.id == id);
                    if (lb == null)
                    {
                        return response.ResponseError(StatusCodes.Status404NotFound, "Không có loại bàn cần xoá ", null);
                    }

                    lb.status = 2;
                    
                    var ban = contextDB.Ban.Where(x => x.LoaiBanID == lb.id).ToList();

                    if (ban.Count() > 0)
                    {
                        foreach (var b in ban)
                        {
                            b.status = 2;
                            var lsthd = contextDB.HoaDon.Where(y => y.BanID == b.id).ToList();
                            /*foreach (var hd in lsthd)
                            {
                                var lstCTHD = contextDB.ChiTietHoaDon.Where(z => z.HoaDonID == hd.id);
                                contextDB.RemoveRange(lstCTHD);
                            }*/
                            if(lsthd.Count() > 0)
                            {
                                foreach (var hd in lsthd)
                                {
                                    hd.status = 2;
                                }
                                contextDB.Update(lsthd);
                            } 
                            //contextDB.RemoveRange(lsthd);
                        }
                        contextDB.Ban.UpdateRange(ban);
                    }
                    //contextDB.RemoveRange(ban);
                    //contextDB.Remove(lb);
                    
                    
                    contextDB.Update(lb);
                    
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
        #endregion
    }
}
