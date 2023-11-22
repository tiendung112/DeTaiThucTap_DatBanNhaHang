﻿using DatBanNhaHang.Entities.NhaHang;
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
        public  async Task<IQueryable<TrangThaiHoaDonDTOs>> HienThiTrangThaiHoaDon()
        {
            return contextDB.TrangThaiHoaDon.Select(x=>converters.EntityToDTOs(x));
        }

        public async Task<ResponseObject<TrangThaiHoaDonDTOs>> SuaTrangThaiHoaDon(Request_SuaTrangThaiHoaDon request)
        {
            var tthd = contextDB.TrangThaiHoaDon.SingleOrDefault(x => x.id == request.TrangThaiHoaDonID);
            if(tthd == null)
            {
                return response.ResponseError(StatusCodes.Status404NotFound, "Không tồn tại trạng thái này ", null);
            }
            tthd.TenTrangThai = request.TenTrangThai==null?tthd.TenTrangThai:request.TenTrangThai;
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
            };
            contextDB.TrangThaiHoaDon.Add(tthd);
            await contextDB.SaveChangesAsync();
            return response.ResponseSuccess("Thêm trạng thái thành công ",converters.EntityToDTOs(tthd));
        }

        public async Task<ResponseObject<TrangThaiHoaDonDTOs>> XoaTrangThaiHoaDon(Request_XoaTrangThaiHoaDon request)
        {
            var tthd = contextDB.TrangThaiHoaDon.SingleOrDefault(x => x.id == request.TrangThaiHoaDonID);
            if (tthd == null)
            {
                return response.ResponseError(StatusCodes.Status404NotFound, "Không tồn tại trạng thái này ", null);
            }
            var lsthd = contextDB.HoaDon.Where(x=>x.id==tthd.id).ToList();
            foreach(var hd in lsthd)
            {
                var lstCT = contextDB.ChiTietHoaDon.Where(x => x.HoaDonID == hd.id);
                contextDB.RemoveRange(lstCT);
            }
            contextDB.RemoveRange(lsthd);
            contextDB.Remove(tthd);
            await contextDB.SaveChangesAsync();
            return response.ResponseSuccess("Xoá trạng thái thành công ", converters.EntityToDTOs(tthd));
        }
    }
}
