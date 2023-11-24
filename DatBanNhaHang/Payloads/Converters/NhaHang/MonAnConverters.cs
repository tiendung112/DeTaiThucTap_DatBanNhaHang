using DatBanNhaHang.Context;
using DatBanNhaHang.Entities.NhaHang;
using DatBanNhaHang.Payloads.DTOs.NhaHang;

namespace DatBanNhaHang.Payloads.Converters.NhaHang
{
    public class MonAnConverters
    {
        private readonly AppDbContext context = new AppDbContext();
        public MonAnDTOs EntityToDTOs(MonAn monAn)
        {
            return new MonAnDTOs
            {
                MonAnID =monAn.id,
                LoaiMonAnID = monAn.LoaiMonAnID,
                TenLoaiMonAn = context.LoaiMonAn.SingleOrDefault(x=>x.id==monAn.LoaiMonAnID).TenLoai,
                TenMon = monAn.TenMon,
                GiaTien = monAn.GiaTien,
                MoTa= monAn.MoTa,
                AnhMonAn1URL = monAn.AnhMonAn1URL,
                AnhMonAn2URL = monAn.AnhMonAn2URL,
                AnhMonAn3URL = monAn.AnhMonAn3URL
            };
        }
    }
}
