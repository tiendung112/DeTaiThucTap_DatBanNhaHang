using DatBanNhaHang.Context;
using DatBanNhaHang.Entities.NhaHang;
using DatBanNhaHang.Payloads.DTOs.NhaHang;

namespace DatBanNhaHang.Payloads.Converters.NhaHang
{
    public class ChiTietHoaDonConverters
    {private readonly AppDbContext context = new AppDbContext();
        public ChiTietHoaDonDTOs EntityToDTOs(ChiTietHoaDon chiTietHoaDon)
        {
            return new ChiTietHoaDonDTOs()
            {
                ChiTietHoaDonID = chiTietHoaDon.id,
                TenMon =context.MonAn.SingleOrDefault(x=>x.id==chiTietHoaDon.MonAnID).TenMon ,
                MonAnID = chiTietHoaDon.MonAnID,
                ThanhTien = chiTietHoaDon.ThanhTien,
                SoLuong = chiTietHoaDon.SoLuong
            };
        }
    }
}
