using DatBanNhaHang.Entities.NhaHang;
using DatBanNhaHang.Payloads.DTOs.NhaHang;

namespace DatBanNhaHang.Payloads.Converters.NhaHang
{
    public class MonAnConverters
    {
        public MonAnDTOs EntityToDTOs(MonAn monAn)
        {
            return new MonAnDTOs
            {
                LoaiMonAnID = monAn.LoaiMonAnID,
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
