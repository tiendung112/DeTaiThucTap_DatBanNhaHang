using DatBanNhaHang.Entities.NhaHang;
using DatBanNhaHang.Payloads.DTOs.NhaHang;

namespace DatBanNhaHang.Payloads.Converters.NhaHang
{
    public class LoaiBanConverters
    {
        public LoaiBanDTOs EntityToDTOs(LoaiBan loaiBan)
        {

            return new LoaiBanDTOs()
            {
                LoaiBanID = loaiBan.id,
                TenLoaiBan = loaiBan.TenLoaiBan,
            };
        }
    }
}
