using DatBanNhaHang.Entities.NhaHang;
using DatBanNhaHang.Payloads.DTOs.NguoiDung;

namespace DatBanNhaHang.Payloads.Converters.NguoiDung
{
    public class NhanXetConverters
    {
        public NhanXetDTOs EntityToDTOs(NhanXet nhanXet)
        {
            return new NhanXetDTOs()
            {
                AnhURL = nhanXet.AnhURL,
                ChuThich = nhanXet.ChuThich,
                HoTen = nhanXet.HoTen,
                id = nhanXet.id,
                NoiDung = nhanXet.NoiDung,
            };
        }
    }
}
