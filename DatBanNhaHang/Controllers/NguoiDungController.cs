﻿using DatBanNhaHang.Entities.NguoiDung;
using DatBanNhaHang.Payloads.DTOs.NguoiDung;
using DatBanNhaHang.Payloads.Requests.NguoiDung;
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
    public class NguoiDungController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthServices _authService;
        private readonly User user;
        public NguoiDungController(IConfiguration configuration, IAuthServices authService)
        {
            _configuration = configuration;
            _authService = authService;
            user = new User();
        }

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

        [HttpGet]
        [Route("/api/auth/get-all")]
        [Authorize]
        public async Task<IActionResult> GetAlls(int pageSize, int pageNumber)
        {
            return Ok(await _authService.GetAlls(pageSize, pageNumber));
        }

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

        [HttpPost]
        
        [Route("/api/auth/XacNhanDangKyTaiKhoan")]
        public async Task<IActionResult> XacNhanDangKyTaiKhoan([FromBody] Request_ValidateRegister request)
        {
            return Ok(await _authService.XacNhanDangKyTaiKhoan(request));
        }

        [HttpDelete]
        [Route("/api/auth/XoaTKChuaKichHoat")]
        [Authorize(Roles ="ADMIN,MOD")]
        public IActionResult DeleteTK ()
        {
            var res =_authService.RemoveTKNotActive();
            return Ok(res);

        }

        [HttpPut]
        [Route("/api/auth/ThayDoiQuyenHan")]
        [Authorize(Roles ="ADMIN")]
        public async Task<IActionResult> ThayDoiQuyenHan([FromBody]Request_ThayDoiQuyen request)
        {
            return Ok(await _authService.ThayDoiQuyenHan(request));
        }
    }
}