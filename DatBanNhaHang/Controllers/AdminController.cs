using DatBanNhaHang.Payloads.DTOs.NguoiDung;
using DatBanNhaHang.Payloads.Requests.NguoiDung;
using DatBanNhaHang.Payloads.Requests.NguoiDung.Admin;
using DatBanNhaHang.Payloads.Requests.NhaHang.Ban;
using DatBanNhaHang.Payloads.Requests.NhaHang.DauBep;
using DatBanNhaHang.Payloads.Requests.NhaHang.KhachHang;
using DatBanNhaHang.Payloads.Requests.NhaHang.LoaiBan;
using DatBanNhaHang.Payloads.Requests.NhaHang.LoaiMonAn;
using DatBanNhaHang.Payloads.Requests.NhaHang.MonAn;
using DatBanNhaHang.Services.Implements;
using DatBanNhaHang.Services.IServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatBanNhaHang.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminServices ADMservices;
        private readonly IConfiguration _configuration;
        private readonly IDauBep daubepservices;
        public readonly ILoaiBan LoaiBanservices;
        private readonly ILoaiMonAn loaiMonAnservices;
        private readonly IMonAn MonAnservices;
        private readonly IBan banServices;
        private readonly IKhachHang khachhangServices;
        public AdminController(IConfiguration configuration, IAdminServices _services)
        {
            ADMservices = _services;
            _configuration = configuration;
            daubepservices = new DauBepServices();
            LoaiBanservices = new LoaiBanServices();
            loaiMonAnservices = new LoaiMonAnServices();
            MonAnservices = new MonAnServices();
            banServices = new BanServices();
            khachhangServices = new KhachHangServices();
        }
        #region đăng nhập , đăng ký 
        [HttpPost]
        [Route("/api/Admin/register")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Register([FromForm] Request_AdminRegister register)
        {
            int id = Convert.ToInt32(HttpContext.User.FindFirst("Id").Value);
            var result = await ADMservices.RegisterRequest(id, register);
            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost]
        [Route(("/api/Admin/login"))]
        public async Task<IActionResult> Login(Request_AdminLogin request)
        {
            var result = await ADMservices.Login(request);
            if (result == null)
            {
                return Unauthorized(result);
            }
            return Ok(result);
        }

        [HttpPost]
        [Route("/api/Admin/renew-token")]
        public IActionResult RenewToken(TokenDTO token)
        {
            var result = ADMservices.RenewAccessToken(token);
            if (result == null)
            {
                return Unauthorized(result);
            }
            return Ok(result);
        }
        #endregion
        #region quên , đổi mật khẩu
        [HttpPut]
        [Route("/api/Admin/change-password")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> ChangePassword(Request_AdminChangePassword request)
        {
            int id = Convert.ToInt32(HttpContext.User.FindFirst("Id").Value);
            return Ok(await ADMservices.ChangePassword(id, request));

        }
        [HttpPost]
        [Route("/api/Admin/forgot-password")]
        public async Task<IActionResult> ForgotPassword(Request_AdminForgotPassword request)
        {
            return Ok(await ADMservices.ForgotPassword(request));
        }

        [HttpPost]
        [Route("/api/Admin/create-new-password")]
        public async Task<IActionResult> CreateNewPassword(Request_AdminConfirmCreateNewPassword request)
        {
            return Ok(await ADMservices.CreateNewPassword(request));
        }
        #endregion
        #region xem , chỉnh sửa , xoá 
        [HttpPut]
        [Route("/api/Admin/ThayDoiQuyenHan")]
        //[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> ThayDoiQuyenHan([FromBody] Request_AdminThayDoiQuyen request)
        {
            return Ok(await ADMservices.ThayDoiQuyenHan(request));
        }
        [HttpGet]
        [Route("/api/Admin/get-all")]
        //[Authorize]
        public async Task<IActionResult> GetAlls(int pageSize, int pageNumber)
        {
            return Ok(await ADMservices.GetAlls(pageSize, pageNumber));
        }
        [HttpDelete]
        [Route("/api/Admin/XoaTKChuaKichHoat")]
        //[Authorize(Roles = "ADMIN,MOD")]
        public IActionResult DeleteTK()
        {
            var res = ADMservices.RemoveTKNotActive();
            return Ok(res);

        }
        #endregion
        #region đầu bếp 
        [HttpPost]
        [Route("/api/DauBep/ThemDauBep")]
        //[Authorize(Roles ="ADMIN,MOD")]
        public async Task<IActionResult> ThemDauBep([FromForm] Request_ThemDauBep request)
        {
            var result = await daubepservices.ThemDauBep(request);
            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut]
        [Route("/api/DauBep/SuaDauBep/{id}")]
        //[Authorize(Roles = "ADMIN , MOD")]
        public async Task<IActionResult> SuaDauBep(int id, [FromForm] Request_SuaDauBep request)
        {
            var result = await daubepservices.SuaDauBep(id, request);

            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete]
        [Route("/api/DauBep/XoaDauBep/{id}")]
        //[Authorize(Roles ="ADMIN , MOD")]
        public async Task<IActionResult> XoaDauBep([FromRoute] int id)
        {
            var result = await daubepservices.XoaDauBep(id);
            if (result == null)
            {
                return BadRequest(result);
            }
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
        [Route("api/LoaiBan/ThemLoaiBan")]
        //[Authorize(Roles ="ADMIN")]
        public async Task<IActionResult> ThemLoaiBan([FromBody] Request_ThemLoaiBan request)
        {
            var result = await LoaiBanservices.ThemLoaiBan(request);
            if (result != null)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpPut]
        [Route("api/LoaiBan/SuaLoaiBan/{id}")]
        //[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> SuaLoaiBan([FromRoute] int id, [FromBody] Request_SuaLoaiBan request)
        {
            var result = await LoaiBanservices.SuaLoaiBan(id, request);
            if (result != null)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpDelete]
        [Route("api/LoaiBan/XoaLoaiBan/{id}")]
        //[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> XoaLoaiBan([FromRoute] int id)
        {
            var result = await LoaiBanservices.XoaLoaiBan(id);
            if (result != null)
                return Ok(result);
            return BadRequest(result);
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
        #endregion
        #region loại món ăn
        [HttpPost]
        [Route("/api/LoaiMonAn/ThemLoaiMonAn")]
        public async Task<IActionResult> ThemLoaiMonAn([FromBody] Request_ThemLoaiMonAn request)
        {
            var result = await loaiMonAnservices.ThemLoaiMonAn(request);
            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPost]
        [Route("/api/LoaiMonAn/ThemLoaiMonAnKemMonAn")]
        public async Task<IActionResult> ThemLoaiMonAnKemMonAn([FromBody] Request_ThemLoaiMonAnKemMonAn request)
        {
            var result = await loaiMonAnservices.ThemLoaiMonAnKemMonAn(request);
            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut]
        [Route("/api/LoaiMonAn/SuaLoaiMonAn/{id}")]
        //[Authorize(Roles = "ADMIN,MOD")]
        public async Task<IActionResult> SuaLoaiMonAn([FromRoute] int id, [FromBody] Request_SuaLoaiMonAn request)
        {
            var result = await loaiMonAnservices.SuaLoaiMonAn(id, request);
            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete]
        [Route("/api/LoaiMonAn/XoaLoaiMonAn/{id}")]
        //[Authorize(Roles = "ADMIN,MOD")]
        public async Task<IActionResult> XoaLoaiMonAn([FromRoute] int id)
        {
            var result = await loaiMonAnservices.XoaLoaiMonAn(id);
            if (result == null)
            {
                return BadRequest(result);
            }
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
        #endregion
        #region món ăn

        [HttpPost]
        [Route("/api/MonAn/ThemMonAn")]
        public async Task<IActionResult> ThemMonAn([FromForm] Request_ThemMonAn request)
        {
            var result = await MonAnservices.ThemMonAn(request);
            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPut]
        [Route("/api/MonAn/SuaMonAn/{id}")]
        public async Task<IActionResult> SuaMonAn([FromRoute] int id, [FromForm] Request_SuaMonAn request)
        {
            var result = await MonAnservices.SuaMonAn(id, request);
            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpDelete]
        [Route("/api/MonAn/XoaMonAn/{id}")]
        public async Task<IActionResult> XoaMonAn([FromRoute] int id)
        {
            var result = await MonAnservices.XoaMonAn(id);
            if (result == null)
            {
                return BadRequest(result);
            }
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
        public async Task<IActionResult> ThemBan([FromBody] Request_ThemBan request)
        {
            var result = await banServices.ThemBan(request);
            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);

        }

        [HttpPut]
        [Route("/api/Ban/SuaBan/{id}")]
        //[Authorize(Roles = "ADMIN,MOD")]
        public async Task<IActionResult> SuaBan([FromRoute] int id, [FromBody] Request_SuaBan request)
        {
            var result = await banServices.SuaBan(id, request);
            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete]
        [Route("/api/Ban/XoaBan/{id}")]
        //[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> XoaBan([FromRoute] int id)
        {
            var result = await banServices.XoaBan(id);
            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        #endregion
        #region khách hàng 

        [HttpGet]
        [Route("/api/KhachHang/HienThiKhachHang")]
        //[Authorize(Roles = "ADMIN,MOD")]
        public async Task<IActionResult> HienThiKhachHang(int pageSize, int pageNumber)
        {
            return Ok(await khachhangServices.HienThiKhachHang(0, pageSize, pageNumber));
        }

        [HttpGet]
        [Route("/api/KhachHang/HienThiKhachHang/{id}")]
        //[Authorize(Roles = "ADMIN,MOD")]
        public async Task<IActionResult> HienThiKhachHang([FromRoute] int id, int pageSize, int pageNumber)
        {
            return Ok(await khachhangServices.HienThiKhachHang(id, 0, 0));
        }
        [HttpPost]
        [Route("/api/KhachHang/ThemKhachHang")]
        //[Authorize(Roles ="ADMIN,MOD")]
        public async Task<IActionResult> ThemKhachHang(int userid, [FromBody] Request_ThemKhachHang request)
        {

            var result = await khachhangServices.ThemKhachHang(userid, request);
            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut]
        [Route("/api/KhachHang/SuaKhachHang/{id}")]
        //[Authorize(Roles = "ADMIN,MOD")]
        public async Task<IActionResult> SuaKhachHang([FromRoute] int id, [FromBody] Request_SuaKhachHang request)
        {
            var result = await khachhangServices.SuaKhachHang(id, request);
            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpDelete]
        [Route("/api/KhachHang/XoaKhachHang/{id}")]
        //[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> XoaKhachHang([FromRoute] int id)
        {
            var result = await khachhangServices.XoaKhachHang(id);
            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        #endregion

    }
}
