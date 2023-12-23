using DatBanNhaHang.Entities.NhaHang;
using DatBanNhaHang.Payloads.DTOs.NhaHang;
using DatBanNhaHang.Services.Implements.DatBanNhaHang.Service.Implements;

namespace DatBanNhaHang.Payloads.Converters.NhaHang
{
    public class LoaiBanConverters : BaseService
    {
        private readonly BanConverters _banConverters = new BanConverters();
        public LoaiBanDTOs EntityToDTOs(LoaiBan loaiBan)
        {

            return new LoaiBanDTOs()
            {
                LoaiBanID = loaiBan.id,
                TenLoaiBan = loaiBan.TenLoaiBan,
                bans = contextDB.Ban.Where(x => x.LoaiBanID == loaiBan.id).Select(y => _banConverters.EntityToDTOs(y))
            };
        }

        public SingleLoaiBanDTOs EntitySingleLoaiBanToDTOs(LoaiBan loaiBan)
        {
            return new SingleLoaiBanDTOs()
            {
                LoaiBanID = loaiBan.id,
                TenLoaiBan = loaiBan.TenLoaiBan,
            };
        }
    }
}
