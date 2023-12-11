using DatBanNhaHang.Entities.NhaHang;
using DatBanNhaHang.Payloads.DTOs.NguoiDung;

namespace DatBanNhaHang.Payloads.Converters.NguoiDung
{
    public class LienHeConverters
    {
        public LienHeDTOs EntityToDTOs(LienHe lh)
        {
            return new LienHeDTOs()
            {
                lienHeId = lh.id,
                DaTraLoi = lh.DaTraLoi == true ? "Đã trả lời " : "Chưa trả lời",
                Email = lh.Email,
                Hoten = lh.Hoten,
                NoiDung = lh.NoiDung,
                ThoiGianGui = lh.ThoiGianGui,
                ThoiGianTraLoi = lh.ThoiGianTraLoi,
                TieuDe = lh.TieuDe,

            };
        }
    }
}
