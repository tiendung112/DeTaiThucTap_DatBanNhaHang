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
                UserID = hoaDon.userId,
                TenKhachHang = contextDB.User.SingleOrDefault(x => x.id == hoaDon.userId).Name,
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
                TrangThaiHoaDon = hoaDon.TrangThaiHoaDonID == 1 ? "Chưa xác nhận"
                : hoaDon.TrangThaiHoaDonID == 2 ? "Chưa thanh toán"
                : "Đã thanh toán",
                SoBan = contextDB.Ban.SingleOrDefault(x=>x.id==hoaDon.BanID).SoBan.ToString(),
                //ChiTietHoaDonDTOs = contextDB.ChiTietHoaDon.Where(x => x.HoaDonID == hoaDon.id).Select(x => converters.EntityToDTOs(x))
            };
        }

    }
}
