﻿using Azure;
using DatBanNhaHang.Entities.NhaHang;
using DatBanNhaHang.Handler.Image;
using DatBanNhaHang.Pagination;
using DatBanNhaHang.Payloads.Converters.NhaHang;
using DatBanNhaHang.Payloads.DTOs.NhaHang;
using DatBanNhaHang.Payloads.Requests.NhaHang.LoaiMonAn;
using DatBanNhaHang.Payloads.Requests.NhaHang.MonAn;
using DatBanNhaHang.Payloads.Responses;
using DatBanNhaHang.Services.Implements.DatBanNhaHang.Service.Implements;
using DatBanNhaHang.Services.IServices;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using Org.BouncyCastle.Security.Certificates;

namespace DatBanNhaHang.Services.Implements
{
    public class LoaiMonAnServices :BaseService, ILoaiMonAn
    {
        private readonly LoaiMonAnConverters converters;
        private readonly ResponseObject<LoaiMonAnDTOs> res;
        public LoaiMonAnServices()
        {
            converters = new LoaiMonAnConverters();
            res = new ResponseObject<LoaiMonAnDTOs> ();
        }
        public async Task<IQueryable<LoaiMonAnDTOs>> HienThiLoaiMonAn(Pagintation pagintation)
        {
            var lstLMA = contextDB.LoaiMonAn.Select(x => converters.entityTODTOs(x));
            return lstLMA;
        }

        public async Task<ResponseObject<LoaiMonAnDTOs>> SuaLoaiMonAn(Request_SuaLoaiMonAn request)
        {
            var lMAcanSua = contextDB.LoaiMonAn.FirstOrDefault(x => x.id == request.ID);
            if (lMAcanSua == null)
            {
                return res.ResponseError(403, "Không tồn tại loại món ăn này",null);
            }
            else
            {
                lMAcanSua.TenLoai = request.TenLoai==null? lMAcanSua.TenLoai: request.TenLoai;
                contextDB.Update(lMAcanSua);
                await contextDB.SaveChangesAsync();
                return res.ResponseSuccess("Sửa loại món ăn thành công ", converters.entityTODTOs(lMAcanSua));
            }
        }

        public async Task<ResponseObject<LoaiMonAnDTOs>> ThemLoaiMonAn(Request_ThemLoaiMonAn request)
        {
            if (string.IsNullOrWhiteSpace(request.tenLoaiMonAn))
            
                return res.ResponseError(StatusCodes.Status404NotFound, "chưa điền đủ thông tin", null);

            LoaiMonAn nma = new LoaiMonAn
            {
                TenLoai = request.tenLoaiMonAn,
            };
            contextDB.Add(nma);
            await contextDB.SaveChangesAsync();
            return res.ResponseSuccess("Thêm loại món ăn thành công", converters.entityTODTOs(nma));
        }

        public async Task<ResponseObject<LoaiMonAnDTOs>> ThemLoaiMonAnKemMonAn(Request_ThemLoaiMonAn request)
        {
            LoaiMonAn nma = new LoaiMonAn
            {
                TenLoai = request.tenLoaiMonAn,
            };

            List<MonAn>newMA  =await ThemLstMonAn(nma.id, request.MonAn.ToList());
            nma.MonAn = newMA.AsEnumerable();
            contextDB.Add(nma);
            await contextDB.SaveChangesAsync();
            return res.ResponseSuccess("Thêm loại món ăn thành công", converters.entityTODTOs(nma));
        }

        public async  Task<List<MonAn>> ThemLstMonAn(int loaiMonAnID , List<Request_ThemMonAn> monAn)
        {
            var lstLMA =contextDB.LoaiMonAn.SingleOrDefault(x=>x.id== loaiMonAnID);
            if(lstLMA == null)
            {
                return null;
            }
            List<MonAn> lst = new List<MonAn>();
            foreach(var item in monAn)
            {
                int imageSize = 2 * 1024 * 768;
                try
                {
                    MonAn dt = new MonAn();
                    dt.LoaiMonAnID = loaiMonAnID;
                    dt.MoTa = item.MoTa;
                    var anh1 = await HandleUploadImage.Upfile(item.AnhMonAn1URL, $"DatBanNhaHang/MonAn/{dt.id}");
                    dt.AnhMonAn1URL = anh1 == null ?
                        "https://media.istockphoto.com/Id/930858444/vi/anh/rau-%C4%91%E1%BA%B7t-tr%C3%AAn-beckground-c%C3%B4-l%E1%BA%ADp-m%C3%A0u-tr%E1%BA%AFng-rau-thi%E1%BA%BFt-l%E1%BA%ADp-m%C3%B4-h%C3%ACnh.jpg?s=1024x1024&w=is&k=20&c=_gtHwlq2vYCblU22Afs6WX6utAqQi6y-J06SXD4AW48=" 
                        : anh1;
                    var anh2 = await HandleUploadImage.Upfile(item.AnhMonAn2URL, $"DatBanNhaHang/MonAn/{dt.id}");
                    dt.AnhMonAn1URL = anh1 == null ?
                        "https://media.istockphoto.com/Id/930858444/vi/anh/rau-%C4%91%E1%BA%B7t-tr%C3%AAn-beckground-c%C3%B4-l%E1%BA%ADp-m%C3%A0u-tr%E1%BA%AFng-rau-thi%E1%BA%BFt-l%E1%BA%ADp-m%C3%B4-h%C3%ACnh.jpg?s=1024x1024&w=is&k=20&c=_gtHwlq2vYCblU22Afs6WX6utAqQi6y-J06SXD4AW48="
                        : anh2;
                    var anh3 = await HandleUploadImage.Upfile(item.AnhMonAn3URL, $"DatBanNhaHang/MonAn/{dt.id}");
                    dt.AnhMonAn1URL = anh1 == null ?
                        "https://media.istockphoto.com/Id/930858444/vi/anh/rau-%C4%91%E1%BA%B7t-tr%C3%AAn-beckground-c%C3%B4-l%E1%BA%ADp-m%C3%A0u-tr%E1%BA%AFng-rau-thi%E1%BA%BFt-l%E1%BA%ADp-m%C3%B4-h%C3%ACnh.jpg?s=1024x1024&w=is&k=20&c=_gtHwlq2vYCblU22Afs6WX6utAqQi6y-J06SXD4AW48="
                        : anh3;

                    lst.Add(dt);
                    
                }
                catch(Exception ex)
                {
                    return null;
                }
            }

            return lst;

        }

        public async Task<ResponseObject<LoaiMonAnDTOs>> XoaLoaiMonAn(Request_XoaLoaiMonAn request)
        {
            var lstLMA = contextDB.LoaiMonAn.SingleOrDefault(x => x.id == request.ID);
            if (lstLMA == null)
            {
                return res.ResponseError(403, "không tồn tại loại món ăn này", null) ;
            }
            else
            {
                var lstMA = contextDB.MonAn.Where(x => x.LoaiMonAnID == lstLMA.id).ToList();
                //xoá tất cả các món ăn của loại này
                contextDB.RemoveRange(lstMA);
                contextDB.Remove(lstLMA);
                await contextDB.SaveChangesAsync();
                return res.ResponseSuccess("đã xoá thành công loại món ăn này", converters.entityTODTOs(lstLMA));
            }
        }
    }
}