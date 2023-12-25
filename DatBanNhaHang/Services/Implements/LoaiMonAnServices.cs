using DatBanNhaHang.Entities.NhaHang;
using DatBanNhaHang.Handler.Image;
using DatBanNhaHang.Handler.Pagination;
using DatBanNhaHang.Payloads.Converters.NhaHang;
using DatBanNhaHang.Payloads.DTOs.NhaHang;
using DatBanNhaHang.Payloads.Requests.NhaHang.LoaiMonAn;
using DatBanNhaHang.Payloads.Requests.NhaHang.MonAn;
using DatBanNhaHang.Payloads.Responses;
using DatBanNhaHang.Services.Implements.DatBanNhaHang.Service.Implements;
using DatBanNhaHang.Services.IServices;

namespace DatBanNhaHang.Services.Implements
{
    public class LoaiMonAnServices : BaseService, ILoaiMonAn
    {
        private readonly LoaiMonAnConverters converters;
        private readonly ResponseObject<LoaiMonAnDTOs> res;
        public LoaiMonAnServices()
        {
            converters = new LoaiMonAnConverters();
            res = new ResponseObject<LoaiMonAnDTOs>();
        }
        #region hiển thị , tìm kiếm các loại món ăn 
        public async Task<PageResult<LoaiMonAnDTOs>> HienThiLoaiMonAnKemMonAn(int id, int pageSize, int pageNumber)
        {
            var lstLMA = id == 0 ?
                 contextDB.LoaiMonAn.Where(y => y.status == 1).Select(x => converters.entityTODTOs(x))
                 : contextDB.LoaiMonAn.Where(y => y.id == id && y.status == 1).Select(x => converters.entityTODTOs(x));
            var res = Pagintation.GetPagedData(lstLMA, pageSize, pageNumber);
            return res;
        }

        public async Task<PageResult<SingleLoaiMonAnDTOs>> HienThiLoaiMonAn(int id, int pageSize, int pageNumber)
        {

            var lstLMA = id == 0 ?
                contextDB.LoaiMonAn.Where(y => y.status == 1).Select(x => converters.EntitySingletoDTOs(x))
                : contextDB.LoaiMonAn.Where(y => y.id == id && y.status == 1).Select(x => converters.EntitySingletoDTOs(x));
            var res = Pagintation.GetPagedData(lstLMA, pageSize, pageNumber);
            return res;
        }
        #endregion

        #region thêm , sửa , xoá các loại món ăn 

        //thêm loại món ăn
        public async Task<ResponseObject<LoaiMonAnDTOs>> ThemLoaiMonAn(Request_ThemLoaiMonAn request)
        {
            if (string.IsNullOrWhiteSpace(request.tenLoaiMonAn))

                return res.ResponseError(StatusCodes.Status404NotFound, "chưa điền đủ thông tin", null);

            LoaiMonAn nma = new LoaiMonAn
            {
                TenLoai = request.tenLoaiMonAn,
                status = 1
            };
            contextDB.Add(nma);
            await contextDB.SaveChangesAsync();
            return res.ResponseSuccess("Thêm loại món ăn thành công", converters.entityTODTOs(nma));
        }
        //thêm loại món ăn kèm món ăn
        /*public async Task<ResponseObject<LoaiMonAnDTOs>> ThemLoaiMonAnKemMonAn(Request_ThemLoaiMonAnKemMonAn request)
        {
            LoaiMonAn nma = new LoaiMonAn
            {
                TenLoai = request.tenLoaiMonAn,
            };

            List<MonAn> newMA = await ThemLstMonAn(nma.id, request.MonAn.ToList());
            nma.MonAn = newMA.AsEnumerable();
            contextDB.Add(nma);
            await contextDB.SaveChangesAsync();
            return res.ResponseSuccess("Thêm loại món ăn thành công", converters.entityTODTOs(nma));
        }*/
        //thêm lst món ăn
        /*public async Task<List<MonAn>> ThemLstMonAn(int loaiMonAnID, List<Request_ThemMonAn> monAn)
        {
            var lstLMA = contextDB.LoaiMonAn.SingleOrDefault(x => x.id == loaiMonAnID);
            if (lstLMA == null)
            {
                return null;
            }
            List<MonAn> lst = new List<MonAn>();
            foreach (var item in monAn)
            {
                //int imageSize = 2 * 1024 * 768;
                try
                {
                    MonAn dt = new MonAn();
                    dt.LoaiMonAnID = loaiMonAnID;
                    dt.MoTa = item.MoTa;

                    var anh1 = await HandleUploadImage.Upfile(item.AnhMonAn1URL, $"DatBanNhaHang/MonAn/{dt.id}");
                    dt.AnhMonAn1URL = anh1 == null ?
                        "https://media.istockphoto.com/Id/930858444/vi/anh/rau-%C4%91%E1%BA%B7t-tr%C3%AAn-beckground-c%C3%B4-l%E1%BA%ADp-m%C3%A0u-tr%E1%BA%AFng-rau-thi%E1%BA%BFt-l%E1%BA%ADp-m%C3%B4-h%C3%ACnh.jpg?s=1024x1024&w=is&k=20&c=_gtHwlq2vYCblU22Afs6WX6utAqQi6y-J06SXD4AW48="
                        : anh1;


                    lst.Add(dt);

                }
                catch (Exception ex)
                {
                    return null;
                }
            }

            return lst;

        }*/
        //sửa
        public async Task<ResponseObject<LoaiMonAnDTOs>> SuaLoaiMonAn(int id, Request_SuaLoaiMonAn request)
        {
            var lMAcanSua = contextDB.LoaiMonAn.FirstOrDefault(x => x.id == id);
            if (lMAcanSua == null)
            {
                return res.ResponseError(403, "Không tồn tại loại món ăn này", null);
            }
            else
            {
                lMAcanSua.TenLoai = request.TenLoai == null ? lMAcanSua.TenLoai : request.TenLoai;
                contextDB.Update(lMAcanSua);
                await contextDB.SaveChangesAsync();
                return res.ResponseSuccess("Sửa loại món ăn thành công ", converters.entityTODTOs(lMAcanSua));
            }
        }
        //xoá
        public async Task<ResponseObject<LoaiMonAnDTOs>> XoaLoaiMonAn(int id)
        {
            var LMA = contextDB.LoaiMonAn.SingleOrDefault(x => x.id == id);
            if (LMA == null)
            {
                return res.ResponseError(403, "không tồn tại loại món ăn này", null);
            }
            else
            {
                LMA.status = 2;

                var lstMA = contextDB.MonAn.Where(x => x.LoaiMonAnID == LMA.id).ToList();
                if(lstMA.Count > 0)
                {
                    foreach(var item in lstMA)
                    {
                        item.status = 2;
                        //xoá tất cả các món ăn của loại này
                        contextDB.Update(lstMA);
                    }
                }
                contextDB.Update(LMA);
                await contextDB.SaveChangesAsync();
                return res.ResponseSuccess("đã xoá thành công loại món ăn này", converters.entityTODTOs(LMA));
            }
        }


        #endregion
    }
}
