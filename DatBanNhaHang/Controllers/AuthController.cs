using DatBanNhaHang.Payloads.DTOs.NguoiDung;
using DatBanNhaHang.Payloads.Requests.NguoiDung.User;
using DatBanNhaHang.Payloads.Requests.NguoiDung.User.LienHe;
using DatBanNhaHang.Payloads.Requests.NhaHang.HoaDon;
using DatBanNhaHang.Services.IServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatBanNhaHang.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthServices _authService;
        private readonly IHoaDon hoadonServices;
        private readonly ILienHe LienHeServices;
        private readonly IKhachHang khachhangServices;

        public AuthController(IConfiguration configuration, IAuthServices authService, ILienHe lienHe, IHoaDon hoaDon, IKhachHang khachHang)
        {
            _configuration = configuration;
            _authService = authService;
            LienHeServices = lienHe;
            hoadonServices = hoaDon;
            khachhangServices = khachHang;
        }

        #region đăng ký, đăng nhập 
        [HttpPost]
        [Route("/api/auth/register")]
        public async Task<IActionResult> Register([FromForm] Request_Register register)
        {
            var result = await _authService.RegisterRequest(register);
            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost]
        [Route(("/api/auth/login"))]
        public async Task<IActionResult> Login([FromForm] Request_Login request)
        {
            var result = await _authService.Login(request);
            if (result == null)
            {
                return Unauthorized(result);
            }
            return Ok(result);
        }
        [HttpPut]
        [Route("/api/auth/XacNhanDangKyTaiKhoan")]
        public async Task<IActionResult> XacNhanDangKyTaiKhoan([FromForm] Request_ValidateRegister request)
        {
            return Ok(await _authService.XacNhanDangKyTaiKhoan(request));
        }
        [HttpPut]
        [Route("/api/auth/renew-token")]
        public IActionResult RenewToken([FromForm] TokenDTO token)
        {
            var result = _authService.RenewAccessToken(token);
            if (result == null)
            {
                return Unauthorized(result);
            }
            return Ok(result);
        }
        [HttpPut]
        [Route("/api/auth/ThayDoiThongTinUser")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> ThayDoiThongTinUser([FromForm] Request_UpdateInfor request)
        {
            int id = Convert.ToInt32(HttpContext.User.FindFirst("Id").Value);
            return Ok(await _authService.ThayDoiThongTin(id, request));
        }
        #endregion

        [HttpGet]
        [Route("/api/auth/LayTatCaThongTinUser")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> LayTatCaThongTinUser(int pageSize, int pageNumber)
        {
            return Ok(await _authService.GetAlls(pageSize, pageNumber));
        }
        #region quên , đổi mật khẩu
        [HttpPut]
        [Route("/api/auth/change-password")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> ChangePassword([FromForm] Request_ChangePassword request)
        {
            int id = Convert.ToInt32(HttpContext.User.FindFirst("Id").Value);
            return Ok(await _authService.ChangePassword(id, request));

        }
        [HttpPost]
        [Route("/api/auth/forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromForm] Request_ForgotPassword request)
        {
            return Ok(await _authService.ForgotPassword(request));
        }

        [HttpPost]
        [Route("/api/auth/create-new-password")]
        public async Task<IActionResult> CreateNewPassword([FromForm] Request_ConfirmCreateNewPassword request)
        {
            return Ok(await _authService.CreateNewPassword(request));
        }
        #endregion
        #region thay đổi thông tin 

        [HttpPut]
        [Route("/api/auth/ThayDoiThongTin")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> ThayDoiThongTin([FromForm] Request_UpdateInfor request)
        {
            int id = int.Parse(HttpContext.User.FindFirst("Id").Value);
            return Ok(await _authService.ThayDoiThongTin(id, request));
        }
        [HttpPut]
        [Route("/api/auth/ThayDoiEmail")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> ThayDoiEmail()
        {
            int id = int.Parse(HttpContext.User.FindFirst("Id").Value);
            return Ok(await _authService.ThayDoiEmail(id));
        }
        [HttpPut]
        [Route("/api/auth/XacNhanThayDoiEmail")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> XacNhanThayDoiEmail([FromForm] Request_NewMail request)
        {
            return Ok(await _authService.XacNhanDoiEmail(request));
        }

        #endregion
        #region đặt bàn
        [HttpPost]
        [Route("/api/datBan/DatBan")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DatBan([FromForm] Request_ThemHoaDon_User request)
        {
            int id = int.Parse(HttpContext.User.FindFirst("Id").Value);
            return Ok(await hoadonServices.ThemHoaDonUser(id, request));
        }
        [HttpPut]
        [Route("/api/datBan/XacNhanDatBan/{hoadonid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> XacNhanDatBan([FromRoute] int hoadonid, [FromForm] Request_ValidateRegister request)
        {
            int id = int.Parse(HttpContext.User.FindFirst("Id").Value);
            return Ok(await hoadonServices.XacNhanOrder(id, hoadonid, request));
        }
        [HttpPut]
        [Route("/api/datBan/SuaHoaDon/{hoadonid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> SuaHoaDon([FromRoute] int hoadonid, [FromForm] Request_SuaHoaDon_User request, int status)
        {
            int id = int.Parse(HttpContext.User.FindFirst("Id").Value);
            return Ok(await hoadonServices.SuaHoaDon(id, hoadonid, status, request));
        }
        [HttpDelete]
        [Route("/api/datBan/HuyHoaDon/{hoadonid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> HuyHoaDon([FromRoute] int hoadonid)
        {
            int id = int.Parse(HttpContext.User.FindFirst("Id").Value);
            return Ok(await hoadonServices.HuyHoaDon(hoadonid, id));
        }

        [HttpDelete]
        [Route("/api/datBan/XacNhanHuyDon/{hoadonid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> XacNhanHuyDon([FromRoute] int hoadonid, [FromForm] Request_ValidateRegister request)
        {
            int id = int.Parse(HttpContext.User.FindFirst("Id").Value);
            return Ok(await hoadonServices.XacNhanHuyOrder(id, hoadonid, request));
        }
        [HttpGet]
        [Route("/api/datBan/HienThiBanTrong")]
        public async Task<IActionResult> HienThiBanTrong([FromForm] Request_timBanTrong request)
        {
            return Ok(await hoadonServices.TimBanTrong(request));
        }
        [HttpGet]
        [Route("/api/datBan/LichSuHoaDon")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> LichSuHoaDon(int pageSize, int pageNumber)
        {
            int id = int.Parse(HttpContext.User.FindFirst("Id").Value);
            return Ok(await hoadonServices.HienThiHoaDonCuaUser(id, pageSize, pageNumber));
        }
        #endregion
        #region liên hệ

        [HttpPost]
        [Route("/api/LienHe/ThemLienHe")]
        public async Task<IActionResult> ThemLienHe([FromForm] Request_ThemLienHe request)
        {
            return Ok(await LienHeServices.ThemLienHe(request));
        }

        #endregion
    }
}
