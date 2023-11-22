using DatBanNhaHang.Entities.NhaHang;
using DatBanNhaHang.Payloads.DTOs.NhaHang;

namespace DatBanNhaHang.Payloads.Converters.NhaHang
{
    public class TrangThaiHoaDonConverters
    {
        public TrangThaiHoaDonDTOs EntityToDTOs(TrangThaiHoaDon tthd)
        {
            return new TrangThaiHoaDonDTOs() {
                TenTrangThai = tthd.TenTrangThai,
                TrangThaiHoaDonID = tthd.id,
            };
        }
    }
}
