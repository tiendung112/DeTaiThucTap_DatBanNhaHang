using DatBanNhaHang.Entities.NhaHang;
using DatBanNhaHang.Payloads.DTOs.NhaHang;

namespace DatBanNhaHang.Payloads.Converters.NhaHang
{
    public class ChiTietHoaDonConverters
    {
        public ChiTietHoaDonDTOs EntityToDTOs(ChiTietHoaDon chiTietHoaDon)
        {
            return new ChiTietHoaDonDTOs()
            {
                ChiTietHoaDonID = chiTietHoaDon.id,
                MonAnID = chiTietHoaDon.MonAnID,
                DonViTinh = chiTietHoaDon.DonViTinh,
                HoaDonID = chiTietHoaDon.HoaDonID,
                ThanhTien = chiTietHoaDon.ThanhTien,
                SoLuong = chiTietHoaDon.SoLuong
            };
        }
    }
}
