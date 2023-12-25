using DatBanNhaHang.Payloads.DTOs.NguoiDung;
using DatBanNhaHang.Payloads.Requests.NguoiDung;
using DatBanNhaHang.Payloads.Requests.NguoiDung.Admin;
using DatBanNhaHang.Payloads.Requests.NguoiDung.Admin.Blog;
using DatBanNhaHang.Payloads.Requests.NguoiDung.Admin.NhanXet;
using DatBanNhaHang.Payloads.Requests.NhaHang.Ban;
using DatBanNhaHang.Payloads.Requests.NhaHang.DauBep;
using DatBanNhaHang.Payloads.Requests.NhaHang.HoaDon;
using DatBanNhaHang.Payloads.Requests.NhaHang.KhachHang;
using DatBanNhaHang.Payloads.Requests.NhaHang.LoaiBan;
using DatBanNhaHang.Payloads.Requests.NhaHang.LoaiMonAn;
using DatBanNhaHang.Payloads.Requests.NhaHang.MonAn;
using DatBanNhaHang.Payloads.Requests.NhaHang.TrangThaiHoaDon;
using DatBanNhaHang.Services.IServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatBanNhaHang.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminServices ADMservices;
        private readonly IConfiguration _configuration;
        private readonly IDauBep daubepservices;
        private readonly ILoaiBan LoaiBanservices;
        private readonly ILoaiMonAn loaiMonAnservices;
        private readonly IMonAn MonAnservices;
        private readonly IBan banServices;
        //private readonly IKhachHang khachhangServices;
        private readonly ITrangThaiHoaDon TTHDservices;
        private readonly IHoaDon hoaDonServices;
        private readonly IBaiViet BaiVietServices;
        private readonly ILienHe LienHeServices;
        private readonly IThongKe thongKeServices;
        private readonly INhanXet nhanXetServices;
        public AdminController
            (IConfiguration configuration,
                IAdminServices admservices,
                IDauBep dauBep,
                ILoaiBan loaiBan,
                ILoaiMonAn loaiMonAn,
                IMonAn monAn,
                IBan ban,
                //IKhachHang khachHang,
                ITrangThaiHoaDon trangThaiHoaDon,
                IHoaDon hoaDon,
                IBaiViet baiViet,
                ILienHe lienHe,
                IThongKe thongKe,
                INhanXet nhanXet
            )
        {
            ADMservices = admservices;
            _configuration = configuration;
            daubepservices = dauBep;
            LoaiBanservices = loaiBan;
            loaiMonAnservices = loaiMonAn;
            MonAnservices = monAn;
            banServices = ban;
            //khachhangServices = khachHang;
            TTHDservices = trangThaiHoaDon;
            hoaDonServices = hoaDon;
            BaiVietServices = baiViet;
            LienHeServices = lienHe;
            thongKeServices = thongKe;
            nhanXetServices = nhanXet;
        }
        #region đăng nhập , đăng ký 
        [HttpPost]
        [Route("/api/Admin/TaoTaiKhoan")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Register([FromForm] Request_AdminRegister register)
        {
            int id = Convert.ToInt32(HttpContext.User.FindFirst("Id").Value);
            var result = await ADMservices.RegisterRequest(id, register);
            return Ok(result);
        }

        [HttpPost]
        [Route(("/api/Admin/login"))]
        public async Task<IActionResult> Login([FromForm] Request_AdminLogin request)
        {
            var result = await ADMservices.Login(request);

            return Ok(result);
        }

        [HttpPost]
        [Route("/api/Admin/renew-token")]
        public IActionResult RenewToken([FromForm] TokenDTO token)
        {
            var result = ADMservices.RenewAccessToken(token);

            return Ok(result);
        }
        #endregion
        #region quên , đổi mật khẩu
        [HttpPut]
        [Route("/api/Admin/change-password")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> ChangePassword([FromForm] Request_AdminChangePassword request)
        {
            int id = Convert.ToInt32(HttpContext.User.FindFirst("Id").Value);
            return Ok(await ADMservices.ChangePassword(id, request));

        }
        [HttpPost]
        [Route("/api/Admin/forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromForm] Request_AdminForgotPassword request)
        {
            var result = await ADMservices.ForgotPassword(request);
            return Ok(result);
        }


        [HttpPost]
        [Route("/api/Admin/create-new-password")]
        public async Task<IActionResult> CreateNewPassword([FromForm] Request_AdminConfirmCreateNewPassword request)
        {
            return Ok(await ADMservices.CreateNewPassword(request));
        }
        #endregion
        #region thay đổi quyền hạn , xoá acc, laythongtin
        [HttpPut]
        [Route("/api/Admin/ThayDoiQuyenHan")]
        //[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> ThayDoiQuyenHan([FromForm] Request_AdminThayDoiQuyen request)
        {
            return Ok(await ADMservices.ThayDoiQuyenHan(request));
        }
        [HttpGet]
        [Route("/api/Admin/LayTatCaThongTinADMin")]
        //[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> LayTatCaThongTinADMin(int pageSize, int pageNumber)
        {
            return Ok(await ADMservices.GetAlls(pageSize, pageNumber));
        }
        [HttpGet]
        [Route("/api/Admin/ThongTinAdmin")]
        //[Authorize(Roles = "ADMIN")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> ThongTinAdmin()
        {
            int id = Convert.ToInt32(HttpContext.User.FindFirst("Id").Value);
            return Ok(ADMservices.GetAdminTheoId(id));
        }
        [HttpDelete]
        [Route("/api/Admin/XoaTKChuaKichHoat")]
        [Authorize(Roles = "ADMIN,MOD")]
        public IActionResult DeleteTK()
        {
            var res = ADMservices.RemoveTKNotActive();
            return Ok(res);

        }[HttpDelete]
        [Route("/api/Admin/XoaTaiKhoan/{id}")]
        [Authorize(Roles = "ADMIN,MOD")]
        public IActionResult XoaTaiKhoan([FromRoute]int id )
        {
            var res = ADMservices.XoaTaiKhoan(id);
            return Ok(res);
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("/api/Admin/SuaThongTin")]
        public async Task<IActionResult> SuaThongTin([FromForm] Request_AdminUpdateInfor request)
        {
            int id = Convert.ToInt32(HttpContext.User.FindFirst("Id").Value);
            return Ok(await ADMservices.SuaThongTin(id, request));
        }

        
        #endregion
        #region đầu bếp 
        [HttpPost]
        [Route("/api/DauBep/ThemDauBep")]
        //[Authorize(Roles ="ADMIN,MOD")]
        public async Task<IActionResult> ThemDauBep([FromForm] Request_ThemDauBep request)
        {
            var result = await daubepservices.ThemDauBep(request);

            return Ok(result);
        }

        [HttpPut]
        [Route("/api/DauBep/SuaDauBep/{id}")]
        //[Authorize(Roles = "ADMIN , MOD")]
        public async Task<IActionResult> SuaDauBep(int id, [FromForm] Request_SuaDauBep request)
        {
            var result = await daubepservices.SuaDauBep(id, request);
            return Ok(result);
        }

        [HttpDelete]
        [Route("/api/DauBep/XoaDauBep/{id}")]
        //[Authorize(Roles ="ADMIN , MOD")]
        public async Task<IActionResult> XoaDauBep([FromRoute] int id)
        {
            var result = await daubepservices.XoaDauBep(id);

            return Ok(result);
        }

        [HttpGet]
        [Route("/api/DauBep/HienThiDanhSachDauBep")]
        public async Task<IActionResult> LayDSDauBep(int pageSize, int pageNumber)
        {
            int id = 0;
            return Ok(await daubepservices.GetDSDauBep(id, pageSize, pageNumber));
        }

        [HttpGet]
        [Route("/api/DauBep/HienThiDanhSachDauBep/{id}")]
        public async Task<IActionResult> LayDSDauBepTheoID([FromRoute] int id)
        {
            return Ok(await daubepservices.GetDSDauBep(id, 0, 0));
        }
        #endregion
        #region Loại bàn 
        [HttpPost]
        [Route("/api/LoaiBan/ThemLoaiBan")]
        //[Authorize(Roles ="ADMIN")]
        public async Task<IActionResult> ThemLoaiBan([FromForm] Request_ThemLoaiBan request)
        {
            var result = await LoaiBanservices.ThemLoaiBan(request);
            return Ok(result);
        }
        [HttpPut]
        [Route("api/LoaiBan/SuaLoaiBan/{id}")]
        //[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> SuaLoaiBan([FromRoute] int id, [FromForm] Request_SuaLoaiBan request)
        {
            var result = await LoaiBanservices.SuaLoaiBan(id, request);
            return Ok(result);
        }

        [HttpDelete]
        [Route("api/LoaiBan/XoaLoaiBan/{id}")]
        //[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> XoaLoaiBan([FromRoute] int id)
        {
            var result = await LoaiBanservices.XoaLoaiBan(id);
            return Ok(result);
        }

        [HttpGet]
        [Route("api/LoaiBan/HienThiLoaiBan")]
        public async Task<IActionResult> HienThiLoaiBan(int pageSize, int pageNumber)
        {
            return Ok(await LoaiBanservices.HienThiLoaiBan(0, pageSize, pageNumber));
        }
        [HttpGet]
        [Route("api/LoaiBan/HienThiLoaiBan/{id}")]
        public async Task<IActionResult> HienThiLoaiBan(int id)
        {
            return Ok(await LoaiBanservices.HienThiLoaiBan(id, 0, 0));
        }
        [HttpGet]
        [Route("api/LoaiBan/HienThiLoaiBanKemBan")]
        public async Task<IActionResult> HienThiLoaiBanKemBan(int pageSize, int pageNumber)
        {
            return Ok(await LoaiBanservices.HienThiLoaiBanKemBan(0, pageSize, pageNumber));
        }
        [HttpGet]
        [Route("api/LoaiBan/HienThiLoaiBanKemBan/{id}")]
        public async Task<IActionResult> HienThiLoaiBanKemBan(int id)
        {
            return Ok(await LoaiBanservices.HienThiLoaiBanKemBan(id, 0, 0));
        }
        #endregion
        #region loại món ăn
        [HttpPost]
        [Route("/api/LoaiMonAn/ThemLoaiMonAn")]
        public async Task<IActionResult> ThemLoaiMonAn([FromForm] Request_ThemLoaiMonAn request)
        {
            var result = await loaiMonAnservices.ThemLoaiMonAn(request);

            return Ok(result);
        }
        /*[HttpPost]
        [Route("/api/LoaiMonAn/ThemLoaiMonAnKemMonAn")]
        [Authorize(Roles = "ADMIN,MOD")]
        public async Task<IActionResult> ThemLoaiMonAnKemMonAn([FromForm] Request_ThemLoaiMonAnKemMonAn request)
        {
            var result = await loaiMonAnservices.ThemLoaiMonAnKemMonAn(request);
            return Ok(result);
        }
*/
        [HttpPut]
        [Route("/api/LoaiMonAn/SuaLoaiMonAn/{id}")]
        //[Authorize(Roles = "ADMIN,MOD")]
        public async Task<IActionResult> SuaLoaiMonAn([FromRoute] int id, [FromForm] Request_SuaLoaiMonAn request)
        {
            var result = await loaiMonAnservices.SuaLoaiMonAn(id, request);
            return Ok(result);
        }

        [HttpDelete]
        [Route("/api/LoaiMonAn/XoaLoaiMonAn/{id}")]
        //[Authorize(Roles = "ADMIN,MOD")]
        public async Task<IActionResult> XoaLoaiMonAn([FromRoute] int id)
        {
            var result = await loaiMonAnservices.XoaLoaiMonAn(id);

            return Ok(result);
        }

        [HttpGet]
        [Route("/api/LoaiMonAn/HienThiLoaiMonAn")]

        public async Task<IActionResult> HienThiLoaiMonAn(int pageSize, int pageNumber)
        {
            return Ok(await loaiMonAnservices.HienThiLoaiMonAn(0, pageSize, pageNumber));
        }

        [HttpGet]
        [Route("/api/LoaiMonAn/HienThiLoaiMonAn/{id}")]
        public async Task<IActionResult> HienThiLoaiMonAn([FromRoute] int id)
        {
            return Ok(await loaiMonAnservices.HienThiLoaiMonAn(id, 0, 0));
        }
        [HttpGet]
        [Route("/api/LoaiMonAn/HienThiLoaiMonAnKemMonAn")]

        public async Task<IActionResult> HienThiLoaiMonAnKemMonAn(int pageSize, int pageNumber)
        {
            return Ok(await loaiMonAnservices.HienThiLoaiMonAnKemMonAn(0, pageSize, pageNumber));
        }
        [HttpGet]
        [Route("/api/LoaiMonAn/HienThiLoaiMonAnKemMonAnID/{id}")]

        public async Task<IActionResult> HienThiLoaiMonAnKemMonAnID([FromRoute] int id)
        {
            return Ok(await loaiMonAnservices.HienThiLoaiMonAnKemMonAn(id, 0, 0));
        }
        #endregion
        #region món ăn

        [HttpPost]
        [Route("/api/MonAn/ThemMonAn")]
        public async Task<IActionResult> ThemMonAn([FromForm] Request_ThemMonAn request)
        {
            var result = await MonAnservices.ThemMonAn(request);

            return Ok(result);
        }
        [HttpPut]
        [Route("/api/MonAn/SuaMonAn/{id}")]
        public async Task<IActionResult> SuaMonAn([FromRoute] int id, [FromForm] Request_SuaMonAn request)
        {
            var result = await MonAnservices.SuaMonAn(id, request);

            return Ok(result);
        }
        [HttpDelete]
        [Route("/api/MonAn/XoaMonAn/{id}")]
        public async Task<IActionResult> XoaMonAn([FromRoute] int id)
        {
            var result = await MonAnservices.XoaMonAn(id);

            return Ok(result);
        }

        [HttpGet]
        [Route("/api/MonAn/HienThiMonAn")]
        public async Task<IActionResult> HienThiMonAn(int pageSize, int pageNumber)
        {
            return Ok(await MonAnservices.HienThiMonAn(0, pageSize, pageNumber));
        }

        [HttpGet]
        [Route("/api/MonAn/HienThiMonAn/{id}")]
        public async Task<IActionResult> HienThiMonAn(int id)
        {
            return Ok(await MonAnservices.HienThiMonAn(id, 0, 0));
        }

        [HttpGet]
        [Route("/api/MonAn/TimKiemMonAn/")]
        public async Task<IActionResult> TimKiemMonAn(string tenMonAn)
        {
            return Ok(await MonAnservices.TimKiemMonAn(tenMonAn, 0, 0));
        }
        #endregion
        #region bàn
        [HttpGet]
        [Route("/api/Ban/HienThiBan/{id}")]
        public async Task<IActionResult> HienThiBan([FromRoute] int id)
        {
            return Ok(await banServices.HienThiBan(id, 0, 0));
        }
        [HttpGet]
        [Route("/api/Ban/HienThiBan")]
        public async Task<IActionResult> HienThiBan(int pageSize, int pageNumber)
        {
            return Ok(await banServices.HienThiBan(0, pageSize, pageNumber));
        }


        [HttpGet]
        [Route("/api/Ban/HienThiBanTheoViTri")]
        public async Task<IActionResult> HienThiBanTheoViTri(int pageSize, int pageNumber)
        {
            return Ok(await banServices.HienThiBanTheoViTri(pageSize, pageNumber));
        }

        [HttpGet]
        [Route("/api/Ban/HienThiBanTheoLoaiBan/{lbid}")]
        public async Task<IActionResult> HienThiBanTheoLoaiBan([FromRoute] int lbid, int pageSize, int pageNumber)
        {
            return Ok(await banServices.HienThiBanTheoLoaiBan(lbid, pageSize, pageNumber));
        }

        [HttpPost]
        [Route("/api/Ban/ThemBan")]
        //[Authorize(Roles = "ADMIN,MOD")]
        public async Task<IActionResult> ThemBan([FromForm] Request_ThemBan request)
        {
            var result = await banServices.ThemBan(request);
            return Ok(result);

        }

        [HttpPut]
        [Route("/api/Ban/SuaBan/{id}")]
        //[Authorize(Roles = "ADMIN,MOD")]
        public async Task<IActionResult> SuaBan([FromRoute] int id, [FromForm] Request_SuaBan request)
        {
            var result = await banServices.SuaBan(id, request);
            return Ok(result);
        }

        [HttpDelete]
        [Route("/api/Ban/XoaBan/{id}")]
        //[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> XoaBan([FromRoute] int id)
        {
            var result = await banServices.XoaBan(id);
            return Ok(result);
        }
        #endregion
        //#region khách hàng 

        //[HttpGet]
        //[Route("/api/KhachHang/HienThiKhachHang")]
        ////[Authorize(Roles = "ADMIN,MOD")]
        //public async Task<IActionResult> HienThiKhachHang(int pageSize, int pageNumber)
        //{
        //    return Ok(await khachhangServices.HienThiKhachHang(0, pageSize, pageNumber));
        //}

        //[HttpGet]
        //[Route("/api/KhachHang/HienThiKhachHang/{id}")]
        ////[Authorize(Roles = "ADMIN,MOD")]
        //public async Task<IActionResult> HienThiKhachHang([FromRoute] int id, int pageSize, int pageNumber)
        //{
        //    return Ok(await khachhangServices.HienThiKhachHang(id, 0, 0));
        //}
        //[HttpGet]
        //[Route("/api/KhachHang/HienThiKhachHangSDT")]
        ////[Authorize(Roles = "ADMIN,MOD")]
        //public async Task<IActionResult> HienThiKhachHangSDT(string std )
        //{
        //    return Ok(await khachhangServices.TimKiemKhachHangSDT(std));
        //}
        //[HttpPost]
        //[Route("/api/KhachHang/ThemKhachHang")]
        ////[Authorize(Roles ="ADMIN,MOD")]
        //public async Task<IActionResult> ThemKhachHang([FromForm] Request_ThemKhachHang request)
        //{

        //    var result = await khachhangServices.ThemKhachHang(request);
        //    return Ok(result);
        //}

        //[HttpPut]
        //[Route("/api/KhachHang/SuaKhachHang/{id}")]
        ////[Authorize(Roles = "ADMIN,MOD")]
        //public async Task<IActionResult> SuaKhachHang([FromRoute] int id, [FromForm] Request_SuaKhachHang request)
        //{
        //    var result = await khachhangServices.SuaKhachHang(id, request);
        //    return Ok(result);
        //}
        //[HttpPut]
        //[Route("/api/KhachHang/NangCapThongTinKhachHangACC")]
        ////[Authorize(Roles = "ADMIN,MOD")]
        //public async Task<IActionResult> NangCapThongTinKhachHangACC([FromForm] Request_NangCapThongTinKhachHang request)
        //{
        //    var result = await khachhangServices.NangCapThongTinKhachHangACC(request);
        //    return Ok(result);
        //}
        //[HttpDelete]
        //[Route("/api/KhachHang/XoaKhachHang/{id}")]
        ////[Authorize(Roles = "ADMIN")]
        //public async Task<IActionResult> XoaKhachHang([FromRoute] int id)
        //{
        //    var result = await khachhangServices.XoaKhachHang(id);
        //    return Ok(result);
        //}
        //#endregion
        #region trạng thái hoá đơn
        [HttpPost]
        [Route("/api/TrangThaiHoaDon/ThemTrangThaiHoaDon")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> ThemTrangThaiHoaDon([FromForm] Request_ThemTrangThaiHoaDon request)
        {
            var result = await TTHDservices.ThemTrangThaiHoaDon(request);
            return Ok(result);
        }
        [HttpPut]
        [Route("/api/TrangThaiHoaDon/SuaTrangThaiHoaDon/{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> SuaTrangThaiHoaDon([FromRoute] int id, [FromForm] Request_SuaTrangThaiHoaDon request)
        {
            var result = await TTHDservices.SuaTrangThaiHoaDon(id, request);
            return Ok(result);
        }
        [HttpDelete]
        [Route("/api/TrangThaiHoaDon/XoaTrangThaiHoaDon/{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> XoaTrangThaiHoaDon([FromRoute] int id)
        {
            var result = await TTHDservices.XoaTrangThaiHoaDon(id);
            return Ok(result);
        }

        [HttpGet]
        [Route("/api/TrangThaiHoaDon/HienThiTrangThaiHoaDon")]
        [Authorize(Roles = "ADMIN , MOD")]
        public async Task<IActionResult> HienThiTrangThaiHoaDon(int pageSize, int pageNumer)
        {
            return Ok(await TTHDservices.HienThiTrangThaiHoaDon(0, pageSize, pageNumer));
        }
        [HttpGet]
        [Route("/api/TrangThaiHoaDon/HienThiTrangThaiHoaDonTheoID/{id}")]
        [Authorize(Roles = "ADMIN , MOD")]
        public async Task<IActionResult> HienThiTrangThaiHoaDonTheoID([FromRoute] int id)
        {
            return Ok(await TTHDservices.HienThiTrangThaiHoaDon(id, 0, 0));
        }
        #endregion
        #region hoá đơn
        /*[HttpPost]
        [Route("/api/HoaDonAdmin/ThemHoaDonAdmin")]
        // [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> ThemHoaDonAdmin([FromBody] Request_ThemHoaDon_Admin request)
        {
            return Ok(await hoaDonServices.ThemHoaDonAdmin(request));
        }*/
        [HttpPut]
        [Route("/api/HoaDonAdmin/CapNhatThongTinHoaDonAdmin/{id}")]

        //[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> CapNhatThongTinHoaDonAdmin([FromRoute] int id )
        {
            return Ok(await hoaDonServices.CapNhatThongTinHoaDon(id));
        }
        [HttpPut]
        [Route("/api/HoaDonAdmin/SuaHoaDonAdmin/{id}")]
        //[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> SuaHoaDon([FromRoute] int id, [FromBody] Request_SuaHoaDon request)
        {
            return Ok(await hoaDonServices.SuaHoaDonAdmin(id, request));
        }

        [HttpDelete]
        [Route("/api/HoaDonAdmin/XoaHoaDon/{id}")]
        //[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> XoaHoaDon([FromRoute] int id)
        {
            return Ok(await hoaDonServices.XoaHoaDonAdmin(id));
        }
        [HttpDelete]
        [Route("/api/HoaDonAdmin/XoaHoaDonChuaDuyet")]
        // [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> XoaHoaDonChuaDuyet()
        {
            return Ok(await hoaDonServices.XoaTatCaHoaDonChuaDuyet());
        }
        [HttpGet]
        [Route("/api/HoaDonAdmin/HienThiHoaDon")]
        public async Task<IActionResult> HienThiHoaDon(int pageSize, int pageNumber)
        {
            var result = await hoaDonServices.HienThiHoaDon(0, pageSize, pageNumber);
            return Ok(result);
        }

        [HttpGet]
        [Route("/api/HoaDonAdmin/HienThiHoaDonTheoid/{id}")]
        public async Task<IActionResult> HienThiHoaDon(int id)
        {
            var result = await hoaDonServices.HienThiHoaDon(1, 0, 0);
            return Ok(result);
        }
        /*[HttpGet]
        [Route("/api/HoaDonAdmin/HienThiHoaDonKhachHang/{id}")]
        public async Task<IActionResult> HienThiHoaDonTheoKhachHang([FromRoute] int id, int pageSize, int pageNumber)
        {
            var result = await hoaDonServices.HienThiHoaDonCuaKhachHang(id, pageSize, pageNumber);
            return Ok(result);
        }*/
        [HttpGet]
        [Route("/api/datBan/HienThiTatCaBanTrong")]
        //[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> HienThiTatCaBanTrong()
        {
            return Ok(await hoaDonServices.HienThiBanTrong());
        }
        #endregion
        #region Bài Viết
        [HttpPost]
        [Route("/api/BaiViet/ThemBaiViet")]
        [Authorize(Roles = "ADMIN")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> ThemBaiViet([FromForm] Request_ThemBaiViet request)
        {
            int id = int.Parse(HttpContext.User.FindFirst("Id").Value);
            return Ok(await BaiVietServices.ThemBaiViet(id, request));
        }

        [HttpPut]
        [Route("/api/BaiViet/SuaBaiViet/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        // [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> SuaBaiViet([FromRoute] int id, [FromForm] Request_SuaBaiViet request)
        {
            int admid = int.Parse(HttpContext.User.FindFirst("Id").Value);
            return Ok(await BaiVietServices.SuaBaiViet(id, admid, request));
        }

        [HttpDelete]
        [Route("/api/BaiViet/XoaBaiViet/{id}")]
        //[Authorize(Roles ="ADMIN")]
        public async Task<IActionResult> XoaBaiViet([FromRoute] int id)
        {
            return Ok(await BaiVietServices.XoaBaiViet(id));
        }

        [HttpGet]
        [Route("/api/BaiViet/HienThiBaiVietTheoID/{id}")]
        //[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> HienThiBaiVietTheoID([FromRoute] int id)
        {
            return Ok(await BaiVietServices.HienThiBaiViet(id, 0, 0));
        }

        [HttpGet]
        [Route("/api/BaiViet/HienThiBaiViet")]
        //[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> HienThiBaiViet(int pageSize, int pageNumber)
        {
            return Ok(await BaiVietServices.HienThiBaiViet(0, pageSize, pageNumber));
        }
        #endregion
        #region liên hệ 
        [HttpGet]
        [Route("/api/LienHe/HienThiLienHe")]
        public async Task<IActionResult> HienThiLienHe(int pageSize, int pageNumber)
        {
            return Ok(await LienHeServices.HienThiLienHe(0, pageSize, pageNumber));
        }
        [HttpGet]
        [Route("/api/LienHe/HienThiLienHeTheoid/{id}")]
        public async Task<IActionResult> HienThiBaiViet(int id)
        {
            return Ok(await LienHeServices.HienThiLienHe(id, 0, 0));
        }

        [HttpPut]
        [Route("/api/LienHe/XacNhanLienHe/{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> SuaBaiViet([FromRoute] int id)
        {
            return Ok(await LienHeServices.XacNhanLienHe(id));
        }
        [HttpDelete]
        [Route("/api/LienHe/XoaLienHe/{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> XoaLienHe(int id)
        {
            return Ok(await LienHeServices.XoaLienHe(id));
        }
        [HttpDelete]
        [Route("/api/LienHe/XoaLienHeQuaHan")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> XoaLienHeQuaHan()
        {
            return Ok(await LienHeServices.XoaLienHeQuaLau());
        }
        #endregion
        #region thống kê doanh thu
        [HttpGet]
        [Route("/api/ThongKe/DoanhThuTheoNgay")]
        //[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> DoanhThuTheoNgay(DateTime ngay)
        {
            var result = await thongKeServices.DoanhThuTheoNgay(ngay);
            return Ok(result);
        }

        [HttpGet]
        [Route("/api/ThongKe/DoanhThuTheoThang")]
        //[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> DoanhThuTheoThang(int thang, int nam)
        {

            return Ok(await thongKeServices.DoanhThuTheoThang(thang, nam));
        }
        [HttpGet]
        [Route("/api/ThongKe/DoanhThuTheoNam")]
        // [Authorize(Roles ="ADMIN")]
        public async Task<IActionResult> DoanhThuTheoNam(int nam)
        {

            return Ok(await thongKeServices.DoanhThuTheoNam(nam));
        }

        [HttpGet]
        [Route("/api/ThongKe/ThongKeKhachHang")]
        // [Authorize(Roles ="ADMIN")]
        public async Task<IActionResult> ThongKeKhachHang()
        {
            return Ok(await thongKeServices.ThongKeKhachHang());
        }

        [HttpGet]
        [Route("/api/ThongKe/SoLuongHoaDonTheoNgay")]
        // [Authorize(Roles ="ADMIN")]
        public async Task<IActionResult> SoLuongHoaDonTheoNgay()
        {

            return Ok(await thongKeServices.SoLuongHoaDonTheoNgay());
        }

        [HttpGet]
        [Route("/api/ThongKe/ThongKeBanDangConSuDung")]
        // [Authorize(Roles ="ADMIN")]
        public async Task<IActionResult> ThongKeBanDangConSuDung()
        {
            return Ok(await thongKeServices.ThongKeBanDangConSuDung());
        }
        #endregion

        #region nhận xét
        [HttpGet]
        [Route("/api/NhanXet/HienThiNhanXet")]
        //[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> HienThiNhanXet(int pageSize, int pageNumber)
        {
            return Ok(await nhanXetServices.HienThiNhanXet(0, pageSize, pageNumber));
        }
        [HttpGet]
        [Route("/api/NhanXet/HienThiNhanXetTheoid/{id}")]
        //[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> HienThiNhanXet([FromRoute] int id)
        {
            return Ok(await nhanXetServices.HienThiNhanXet(id, 0, 0));
        }
        [HttpPost]
        [Route("/api/NhanXet/ThemNhanXet")]
        //[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> ThemNhaNXet([FromForm] Request_ThemNhanXet request)
        {
            return Ok(await nhanXetServices.ThemNhanXet(request));
        }
        [HttpPut]
        [Route("/api/NhanXet/SuaNhanXet/{id}")]
        //[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> SuaNhaNXet([FromRoute] int id, [FromForm] Request_SuaNhanXet request)
        {
            return Ok(await nhanXetServices.SuaNhanXet(id, request));
        }
        [HttpDelete]
        [Route("/api/NhanXet/XoaNhanXet/{id}")]
        //[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> XoaNhanXet([FromRoute] int id)
        {
            return Ok(await nhanXetServices.XoaNhanXet(id));
        }
        #endregion
    }
}
