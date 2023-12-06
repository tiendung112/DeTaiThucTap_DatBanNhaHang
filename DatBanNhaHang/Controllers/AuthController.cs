using DatBanNhaHang.Entities.NguoiDung;
using DatBanNhaHang.Payloads.DTOs.NguoiDung;
using DatBanNhaHang.Payloads.Requests.NguoiDung.User;
using DatBanNhaHang.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Services.WebApi;

namespace DatBanNhaHang.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthServices _authService;
        private readonly User user;
        public AuthController(IConfiguration configuration, IAuthServices authService)
        {
            _configuration = configuration;
            _authService = authService;
            user = new User();
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
        public async Task<IActionResult> Login(Request_Login request)
        {
            var result = await _authService.Login(request);
            if (result == null)
            {
                return Unauthorized(result);
            }
            return Ok(result);
        }
        [HttpPost]
        [Route("/api/auth/XacNhanDangKyTaiKhoan")]
        public async Task<IActionResult> XacNhanDangKyTaiKhoan([FromBody] Request_ValidateRegister request)
        {
            return Ok(await _authService.XacNhanDangKyTaiKhoan(request));
        }
        [HttpPost]
        [Route("/api/auth/renew-token")]
        public IActionResult RenewToken(TokenDTO token)
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
        public async Task<IActionResult> ThayDoiThongTinUser([FromBody] Request_UpdateInfor request)
        {
            int id = Convert.ToInt32(HttpContext.User.FindFirst("Id").Value);
            return Ok(await _authService.ThayDoiThongTin(id, request));
        }
        #endregion
        [HttpGet]
        [Route("/api/auth/get-all")]
        [Authorize]
        public async Task<IActionResult> GetAlls(int pageSize, int pageNumber)
        {
            return Ok(await _authService.GetAlls(pageSize, pageNumber));
        }
        #region quên , đổi mật khẩu
        [HttpPut]
        [Route("/api/auth/change-password")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> ChangePassword(Request_ChangePassword request)
        {
            int id = Convert.ToInt32(HttpContext.User.FindFirst("Id").Value);
            return Ok(await _authService.ChangePassword(id, request));

        }
        [HttpPost]
        [Route("/api/auth/forgot-password")]
        public async Task<IActionResult> ForgotPassword(Request_ForgotPassword request)
        {
            return Ok(await _authService.ForgotPassword(request));
        }

        [HttpPost]
        [Route("/api/auth/create-new-password")]
        public async Task<IActionResult> CreateNewPassword(Request_ConfirmCreateNewPassword request)
        {
            return Ok(await _authService.CreateNewPassword(request));
        }
        #endregion
        

       
    }
}
