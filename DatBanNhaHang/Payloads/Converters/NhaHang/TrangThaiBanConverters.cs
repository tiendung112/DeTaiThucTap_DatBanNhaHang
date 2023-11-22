using DatBanNhaHang.Entities.NhaHang;
using DatBanNhaHang.Payloads.DTOs.NhaHang;

namespace DatBanNhaHang.Payloads.Converters.NhaHang
{
    public class TrangThaiBanConverters
    {
        public TrangThaiBanDTOs EntityToDTOs(TrangThaiBan trangThaiBan)
        {
            return new TrangThaiBanDTOs()
            {
                TrangThaiBanID = trangThaiBan.id,
                TenTrangThai = trangThaiBan.TenTrangThai,
            };
        }
    }
}
