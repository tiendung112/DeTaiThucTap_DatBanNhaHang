using DatBanNhaHang.Entities.NhaHang;
using DatBanNhaHang.Payloads.DTOs.NhaHang;

namespace DatBanNhaHang.Payloads.Converters.NhaHang
{
    public class KhachHangConverters
    {
        public KhachHangDTOs EntityToDTOs(KhachHang khachHang)
        {
            return new KhachHangDTOs()
            {
                KhachHangID  = khachHang.id,
                DiaChi = khachHang.DiaChi,
                HoTen = khachHang.HoTen,
                NgaySinh = khachHang.NgaySinh,
                SDT =khachHang.SDT
            }; 
        }
    }
}
