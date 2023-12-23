using DatBanNhaHang.Entities.NhaHang;
using DatBanNhaHang.Payloads.DTOs.NhaHang;
using DatBanNhaHang.Services.Implements.DatBanNhaHang.Service.Implements;

namespace DatBanNhaHang.Payloads.Converters.NhaHang
{
    public class HoaDonConverters : BaseService
    {
        private readonly ChiTietHoaDonConverters converters = new ChiTietHoaDonConverters();
        public HoaDonDTO EntityToDTOs(HoaDon hoaDon)
        {
            return new HoaDonDTO
            {
                HoaDonID = hoaDon.id,
                BanID = hoaDon.BanID,
                KhachHangID = hoaDon.KhachHangID,
                TenKhachHang = contextDB.KhachHang.SingleOrDefault(x => x.id == hoaDon.KhachHangID).HoTen,
                MaGiaoDich = hoaDon.MaGiaoDich,
                GhiChu = hoaDon.GhiChu,
                TenHoaDon = hoaDon.TenHoaDon,
                ThoiGianDat = hoaDon.ThoiGianDat,
                ThoiGianBatDauThucTe = hoaDon.ThoiGianBatDauThucTe,
                ThoiGianDuKienBatDau = hoaDon.ThoiGianDuKienBatDau,
                ThoiGianDuKienKetThuc = hoaDon.ThoiGianDuKienKetThuc,
                ThoiGianHuyDat = hoaDon.ThoiGianHuyDat,
                ThoiGianKetThucThucTe = hoaDon.ThoiGianKetThucThucTe,
                TongTien = hoaDon.TongTien,
                TrangThaiHoaDon = hoaDon.TrangThaiHoaDonID == 1 ? "Hoá đơn chưa được xác nhận"
                : hoaDon.TrangThaiHoaDonID == 2 ? "Hoá đơn chưa được thanh toán"
                : "Hoá đơn đã được thanh toán",
                ChiTietHoaDonDTOs = contextDB.ChiTietHoaDon.Where(x => x.HoaDonID == hoaDon.id).Select(x => converters.EntityToDTOs(x))
            };
        }

    }
}
