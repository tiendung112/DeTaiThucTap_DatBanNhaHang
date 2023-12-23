using DatBanNhaHang.Payloads.DTOs.NhaHang.ThongKe;

namespace DatBanNhaHang.Services.IServices
{
    public interface IThongKe
    {
        Task<ThongKeDoanhThuDTOs> DoanhThuTheoNgay(DateTime? ngay);
        Task<ThongKeDoanhThuDTOs> DoanhThuTheoThang(int month , int year);
        Task<ThongKeDoanhThuDTOs> DoanhThuTheoNam( int year);
        Task<ThongKeKhachHangDTOs> ThongKeKhachHang();
        Task<List<ThongKeHoaDonTheoNgayDTOs>> SoLuongHoaDonTheoNgay();
        Task<ThongKeSLBanTrongDTOs> ThongKeBanDangConSuDung();
    }
}
