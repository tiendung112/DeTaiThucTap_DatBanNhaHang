using DatBanNhaHang.Entities.NhaHang;
using DatBanNhaHang.Payloads.DTOs.NhaHang;

namespace DatBanNhaHang.Payloads.Converters.NhaHang
{
    public class DauBepConverters
    {
        public DauBepDTOs EntityToDTOs(DauBep dauBep) 
        {
            return new DauBepDTOs {
                Id = dauBep.id,
                HoTen= dauBep.HoTen,
                ngaySinh = dauBep.ngaySinh,
                AnhDauBepURl = dauBep.AnhDauBepURl,
                MoTa = dauBep.MoTa,
                SDT = dauBep.SDT
            };
        
        }

    }
}
