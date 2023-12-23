using DatBanNhaHang.Context;
using DatBanNhaHang.Entities.NhaHang;
using DatBanNhaHang.Payloads.DTOs.NhaHang;

namespace DatBanNhaHang.Payloads.Converters.NhaHang
{
    public class KhachHangConverters
    {
        private readonly AppDbContext context = new AppDbContext();
        public KhachHangDTOs EntityToDTOs(KhachHang khachHang)
        {
            return new KhachHangDTOs()
            {
                KhachHangID = khachHang.id,
                DiaChi = khachHang.DiaChi,
                HoTen = khachHang.HoTen,
                NgaySinh = khachHang.NgaySinh,
                SDT = khachHang.SDT,
                userID = khachHang.userID == null ? null : khachHang.userID,
                userName = khachHang.userID == null ? null : context.User.SingleOrDefault(x => x.id == khachHang.userID).UserName,
            };
        }
    }
}
