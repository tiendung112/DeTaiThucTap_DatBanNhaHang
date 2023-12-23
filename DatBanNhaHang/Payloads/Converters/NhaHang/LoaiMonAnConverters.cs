using DatBanNhaHang.Context;
using DatBanNhaHang.Entities.NhaHang;
using DatBanNhaHang.Payloads.DTOs.NhaHang;

namespace DatBanNhaHang.Payloads.Converters.NhaHang
{
    public class LoaiMonAnConverters
    {
        private readonly AppDbContext context;
        private readonly MonAnConverters MA_converters;
        
        public LoaiMonAnConverters()
        {
            this.context = new AppDbContext();
            this.MA_converters = new MonAnConverters();
        }
        public LoaiMonAnDTOs entityTODTOs(LoaiMonAn loaiMonAn)
        {
            return new LoaiMonAnDTOs
            {
                ID = loaiMonAn.id,
                TenLoai = loaiMonAn.TenLoai,
                MonAn = context.MonAn.Where(x => x.LoaiMonAnID == loaiMonAn.id).Select(x => MA_converters.EntityToDTOs(x)).AsQueryable(),
            };
        }
        public SingleLoaiMonAnDTOs EntitySingletoDTOs(LoaiMonAn loaiMonAn)
        {
            return new SingleLoaiMonAnDTOs
            {
                ID = loaiMonAn.id,
                TenLoai = loaiMonAn.TenLoai,
            };
        }
    }
}
