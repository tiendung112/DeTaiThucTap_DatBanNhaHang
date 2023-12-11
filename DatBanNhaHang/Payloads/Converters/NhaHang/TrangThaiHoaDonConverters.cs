using DatBanNhaHang.Entities.NhaHang;
using DatBanNhaHang.Payloads.DTOs.NhaHang;
using DatBanNhaHang.Services.Implements.DatBanNhaHang.Service.Implements;

namespace DatBanNhaHang.Payloads.Converters.NhaHang
{
    public class TrangThaiHoaDonConverters :BaseService

    {
        private readonly HoaDonConverters hoaDonConverters = new HoaDonConverters();
        public TrangThaiHoaDonDTOs EntityToDTOs(TrangThaiHoaDon tthd)
        {
            return new TrangThaiHoaDonDTOs() {
                TenTrangThai = tthd.TenTrangThai,
                TrangThaiHoaDonID = tthd.id,
                hoaDons = contextDB.HoaDon.Where(x=>x.TrangThaiHoaDonID==tthd.id).Select(y=>hoaDonConverters.EntityToDTOs(y)),
            };
        }
    }
}
