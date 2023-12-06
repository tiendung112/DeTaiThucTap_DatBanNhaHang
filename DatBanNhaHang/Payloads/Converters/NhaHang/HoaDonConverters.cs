using DatBanNhaHang.Entities.NhaHang;
using DatBanNhaHang.Payloads.DTOs.NhaHang;
using DatBanNhaHang.Services.Implements.DatBanNhaHang.Service.Implements;

namespace DatBanNhaHang.Payloads.Converters.NhaHang
{
    public class HoaDonConverters:BaseService
    {
        private readonly ChiTietHoaDonConverters converters  = new ChiTietHoaDonConverters();
        public HoaDonDTO EntityToDTOs(HoaDon hoaDon)
        {
            return new HoaDonDTO
            {
                HoaDonID  = hoaDon.id,
                BanID = hoaDon.BanID,
                KhachHangID = hoaDon.KhachHangID,
                TenKhachHang = contextDB.KhachHang.SingleOrDefault(x=>x.id==hoaDon.KhachHangID).HoTen,
                MaGiaoDich = hoaDon.MaGiaoDich,
                GhiChu = hoaDon.GhiChu,
                TenHoaDon = hoaDon.TenHoaDon,
                ThoiGianTao = hoaDon.ThoiGianTao,
                ThoiGianCapNhap = hoaDon.ThoiGianCapNhap,
                TongTien = hoaDon.TongTien,
                TrangThaiHoaDonID = hoaDon.TrangThaiHoaDonID,
                ChiTietHoaDonDTOs = contextDB.ChiTietHoaDon.Select(x=>converters.EntityToDTOs(x))
            };
        }

    }
}
