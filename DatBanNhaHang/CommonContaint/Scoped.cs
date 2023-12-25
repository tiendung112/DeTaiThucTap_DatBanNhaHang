using DatBanNhaHang.Payloads.Converters.NguoiDung;
using DatBanNhaHang.Payloads.Converters.NhaHang;
using DatBanNhaHang.Payloads.DTOs.NguoiDung;
using DatBanNhaHang.Payloads.DTOs.NhaHang;
using DatBanNhaHang.Payloads.Responses;
using DatBanNhaHang.Services.Implements;
using DatBanNhaHang.Services.IServices;

namespace DatBanNhaHang.CommonContaint
{
    public class Scoped
    {
        public static void DeppendencyServiceEntity(IServiceCollection Services)
        {
            Services.AddScoped<IAuthServices, AuthServices>();
            Services.AddScoped<IAdminServices, AdminServices>();
            Services.AddScoped<IBaiViet, BaiVietServices>();
            Services.AddScoped<IBan, BanServices>();
            Services.AddScoped<IChiTietHoaDon, ChiTietHoaDonServices>();
            Services.AddScoped<IDauBep, DauBepServices>();
            Services.AddScoped<IHoaDon, HoaDonServices>();
            //Services.AddScoped<IKhachHang, KhachHangServices>();
            Services.AddScoped<ILienHe, LienHeServices>();
            Services.AddScoped<ILoaiBan, LoaiBanServices>();
            Services.AddScoped<ILoaiMonAn, LoaiMonAnServices>();
            Services.AddScoped<IMonAn, MonAnServices>();
            Services.AddScoped<INhanXet, NhanXetServices>();
            Services.AddScoped<ITrangThaiHoaDon, TrangThaiHoaDonServices>();
            Services.AddScoped<IThongKe, ThongKeServices>();
        }
        public static void DenppendecyServiceDTOs(IServiceCollection Services)
        {
            Services.AddSingleton<ResponseObject<UserDTO>>();
            Services.AddSingleton<ResponseObject<TokenDTO>>();
            Services.AddSingleton<ResponseObject<AdminDTOs>>();
            Services.AddSingleton<ResponseObject<BaiVietDTOs>>();
            Services.AddSingleton<ResponseObject<BanDTOs>>();
            Services.AddSingleton<ResponseObject<ChiTietHoaDonDTOs>>();
            Services.AddSingleton<ResponseObject<DauBepDTOs>>();
            Services.AddSingleton<ResponseObject<HoaDonDTO>>();
            //Services.AddSingleton<ResponseObject<KhachHangDTOs>>();
            Services.AddSingleton<ResponseObject<LienHeDTOs>>();
            Services.AddSingleton<ResponseObject<LoaiBanDTOs>>();
            Services.AddSingleton<ResponseObject<LoaiMonAnDTOs>>();
            Services.AddSingleton<ResponseObject<MonAnDTOs>>();
            Services.AddSingleton<ResponseObject<NhanXetDTOs>>();
            Services.AddSingleton<ResponseObject<NhanXetDTOs>>();
            Services.AddSingleton<ResponseObject<TrangThaiHoaDonDTOs>>();
        }
        public static void DenppendecyServiceConverters(IServiceCollection Services)
        {
            Services.AddSingleton<AdminConverters>();
            Services.AddSingleton<BaiVietConverters>();
            Services.AddSingleton<LienHeConverters>();
            Services.AddSingleton<NhanXetConverters>();
            Services.AddSingleton<BanConverters>();
            Services.AddSingleton<ChiTietHoaDonConverters>();
            Services.AddSingleton<DauBepConverters>();
            Services.AddSingleton<HoaDonConverters>();
            //Services.AddSingleton<KhachHangConverters>();
            Services.AddSingleton<LoaiBanConverters>();
            Services.AddSingleton<LoaiMonAnConverters>();
            Services.AddSingleton<MonAnConverters>();
            Services.AddSingleton<TrangThaiHoaDonConverters>();
            Services.AddSingleton<UserConverters>();
        }

    }
}
