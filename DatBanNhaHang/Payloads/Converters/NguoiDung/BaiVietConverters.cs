using DatBanNhaHang.Entities.NhaHang;
using DatBanNhaHang.Payloads.DTOs.NguoiDung;
using DatBanNhaHang.Services.Implements.DatBanNhaHang.Service.Implements;

namespace DatBanNhaHang.Payloads.Converters.NguoiDung
{
    public class BaiVietConverters :BaseService
    {
        public BaiVietDTOs EntityToDTOs(BaiViet bLog)
        {
            return new BaiVietDTOs()
            {
                BlogID= bLog.id,
                NgayDang=bLog.NgayDang,
                AnhBlogURl=bLog.AnhBlogURl,
                MoTa=bLog.MoTa,
                NoiDung=bLog.NoiDung,
                TieuDe= bLog.TieuDe ,
                TenNguoiDang =contextDB.Admin.SingleOrDefault(x=>x.id== bLog.AdminId).Name
            };
        }
    }
}
