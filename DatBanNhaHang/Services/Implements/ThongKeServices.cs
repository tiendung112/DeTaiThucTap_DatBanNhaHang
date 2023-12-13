using DatBanNhaHang.Payloads.DTOs.NhaHang.ThongKe;
using DatBanNhaHang.Services.Implements.DatBanNhaHang.Service.Implements;
using DatBanNhaHang.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace DatBanNhaHang.Services.Implements
{
    public class ThongKeServices : BaseService, IThongKe
    {
        public async Task<ThongKeDoanhThuDTOs> DoanhThuTheoNam(int year)
        {
            var lstHoaDonDaThanhToan = contextDB.HoaDon
                .Where(x => x.TrangThaiHoaDonID == 3 && x.ThoiGianKetThucThucTe.Value.Year==year);
            double? doanhthu =await lstHoaDonDaThanhToan.SumAsync(x => x.TongTien);
            int tongsodon =await lstHoaDonDaThanhToan.CountAsync();
            return new ThongKeDoanhThuDTOs()
            {
                ThoiGian =$"Thống kê trong năm {year}",
                soLuongDon = tongsodon,
                tongDoanhThu = doanhthu,
            };
        }

        public async Task<ThongKeDoanhThuDTOs> DoanhThuTheoNgay(DateTime? ngay)
        {
            var lstHoaDonDaThanhToan = contextDB.HoaDon
                .Where(x => x.TrangThaiHoaDonID == 3 && x.ThoiGianKetThucThucTe.Value.Date == ngay);
            double? doanhthu = await lstHoaDonDaThanhToan.SumAsync(x => x.TongTien);
            int tongsodon = await lstHoaDonDaThanhToan.CountAsync();
            return new ThongKeDoanhThuDTOs()
            {
                ThoiGian = $"Thống kê trong ngày {ngay.Value.ToString("dd/MM/yyyy")}",
                soLuongDon = tongsodon,
                tongDoanhThu = doanhthu,
            };
        }

        public async Task<ThongKeDoanhThuDTOs> DoanhThuTheoThang(int month, int year)
        {
            var lstHoaDonDaThanhToan = contextDB.HoaDon.Where(x => x.TrangThaiHoaDonID == 3 && x.ThoiGianKetThucThucTe.Value.Month == month&& x.ThoiGianKetThucThucTe.Value.Year == year);
            double? doanhthu = await lstHoaDonDaThanhToan.SumAsync(x => x.TongTien);
            int tongsodon = await lstHoaDonDaThanhToan.CountAsync();
            return new ThongKeDoanhThuDTOs()
            {
                ThoiGian = $"Thống kê trong tháng {month} năm {year}",
                soLuongDon = tongsodon,
                tongDoanhThu = doanhthu,
            };
        }
    }
}
