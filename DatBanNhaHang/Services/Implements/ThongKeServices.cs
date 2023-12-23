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

        public async Task<List<ThongKeHoaDonTheoNgayDTOs>> SoLuongHoaDonTheoNgay()
        {
            var lstHoadon = contextDB.HoaDon.
                Where(x => x.ThoiGianBatDauThucTe.Value.Month == DateTime.Now.Month 
                && x.ThoiGianBatDauThucTe.Value.Day == DateTime.Now.Day).ToList();
            List<ThongKeHoaDonTheoNgayDTOs> lst = new List<ThongKeHoaDonTheoNgayDTOs>();
            foreach(var tongDo in lstHoadon)
            {
                ThongKeHoaDonTheoNgayDTOs newhd = new ThongKeHoaDonTheoNgayDTOs() {
                    Ban = "Bàn số" + contextDB.Ban.SingleOrDefault(x => x.id == tongDo.BanID).SoBan.ToString(),
                    GiaTien = tongDo.TongTien,
                    tenKhachHang = contextDB.KhachHang.SingleOrDefault(x=>x.id==tongDo.KhachHangID).HoTen,
                    ThoiGianDen = tongDo.ThoiGianBatDauThucTe,
                    ThoiGianVe = tongDo.ThoiGianKetThucThucTe,
                };
            }
            return lst;
        }

        public async Task<ThongKeSLBanTrongDTOs> ThongKeBanDangConSuDung()
        {
            HoaDonServices hoaDonServices = new HoaDonServices();
            int lstBanTrong = hoaDonServices.HienThiBanTrong().Result.Count();
            if (lstBanTrong == 0)
            {
                return new ThongKeSLBanTrongDTOs()
                {
                    soLuongBan = 0
                };
            }
            else
            {
                return new ThongKeSLBanTrongDTOs()
                {
                    soLuongBan = lstBanTrong
                };
            }
        }

        public async Task<ThongKeKhachHangDTOs> ThongKeKhachHang()
        {
            int lstKh = contextDB.KhachHang.Count();
            return  new ThongKeKhachHangDTOs()
            {
                soLuongKh = lstKh
            };
        }
    }
}
